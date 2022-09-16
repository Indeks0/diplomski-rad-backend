using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOI.Backend.Infrastructure;
using WAZOI.Backend.Models;
using WAZOI.Backend.Models.Common;
using WAZOI.Backend.Repository.Common;
using WAZOI.Backend.Services.Common;

namespace WAZOI.Backend.Services
{
    public class ExamService : IExamService
    {
        #region Fields

        private IUnitOfWork _uow;
        private IExamRepository _repository;
        private IExamQuestionService _examQuestionService;
        private ISolvedExamService _solvedExamService;

        #endregion Fields

        #region Constructors

        public ExamService(IUnitOfWork uow, IExamQuestionService examQuestionService, ISolvedExamService solvedExamService)
        {
            this._uow = uow;
            _repository = uow.ExamRepository;
            _examQuestionService = examQuestionService;
            _solvedExamService = solvedExamService;
        }

        #endregion Constructors

        #region Methods

        public async Task<IExamDto> GetSolvedExamsAsync(Guid examId)
        {
            var exam = await _repository.GetSolvedExamsAsync(examId);
            return exam;
        }

        public async Task<int> CreateExamAsync(IExamDto examDomain)
        {
            _repository.Insert(examDomain);
            return await _uow.SaveAsync();
        }

        public async Task<List<IExamDto>> GetExamsForLockingAsync()
        {
            return await _repository.GetExamsForLockingAsync();
        }

        public async Task<IExamDto> GetExamForSolvingAsync(Guid id)
        {
            var exam = await _repository.GetExamForSolvingAsync(id);

            if (exam.RandomizedQuestionsOrder)
            {
                ListOrderRandomizer.Shuffle(exam.Questions);
            }
            foreach (var question in exam.Questions)
            {
                var temp = JObject.Parse(question.Question.Content);
                temp.Descendants()
                .OfType<JProperty>()
                .Where(attr => attr.Name.Contains("correctAnswer"))
                .ToList()
                .ForEach(attr => attr.Remove());
                question.Question.Content = temp.ToString();
            }
            return exam;
        }

        public async Task<(List<IExamDto> OpenExams, List<IExamDto> FutureExams)> GetExamsForStudentAsync(Guid subjectId, Guid studentId)
        {
            var result = await _repository.GetExamsForStudentAsync(subjectId, studentId);
            return result;
        }

        public async Task<IExamDto> GetExamWithCriteriaAsync(Guid examId)
        {
            return await _repository.GetExamWithCriteriaAsync(examId);
        }

        public async Task<bool> CalculateAndSaveSolvedExamAsync(ISolvedExamSubmitDto solvedExamSubmitDomain)
        {
            var examQuestions = await _examQuestionService.GetExamQuestionsAsync(solvedExamSubmitDomain.ExamId);
            var exam = await _repository.GetExamWithCriteriaAsync(solvedExamSubmitDomain.ExamId);

            var solvedExam = new SolvedExamDto();
            solvedExam.ExamStudentId = solvedExamSubmitDomain.StudentExamId;
            solvedExam.isLocked = false;
            solvedExam.ScoredPoints = 0;
            solvedExam.MaximumPoints = 0;

            JObject solvedExamContent = new JObject();
            List<JObject> solvedExamContentList = new List<JObject>();

            foreach (var submittedAnswer in solvedExamSubmitDomain.Answers)
            {
                var question = examQuestions.FirstOrDefault(x => x.Id == submittedAnswer.Id);

                solvedExam.MaximumPoints += question.Points;

                var answerJson = JObject.Parse(question.Question.Content);

                if (question.Question.Type.Equals(DefaultApplicationValues.TrueFalse))
                {
                    var submittedAnswerValue = submittedAnswer.Answer;
                    var correctAnswerValue = answerJson.Value<string>("correctAnswer");

                    JObject solvedExamContentItem = new JObject
                    {
                        new JProperty("type", question.Question.Type),
                        new JProperty("stem", answerJson.Value<string>("stem")),
                        new JProperty("correctAnswer", correctAnswerValue),
                        new JProperty("answered", submittedAnswerValue),
                        new JProperty("maximumPoints", question.Points),
                    };

                    if (correctAnswerValue.Equals(submittedAnswerValue, StringComparison.InvariantCultureIgnoreCase))
                    {
                        solvedExam.ScoredPoints += question.Points;
                        solvedExamContentItem["scoredPoints"] = question.Points;
                    }
                    else
                    {
                        solvedExamContentItem["scoredPoints"] = 0;
                    }

                    solvedExamContentList.Add(solvedExamContentItem);
                }
                else if (question.Question.Type.Equals(DefaultApplicationValues.SingleChoice))
                {
                    var submittedAnswerIndexValue = submittedAnswer.Answer;
                    var correctAnswerIndexValue = answerJson.Value<string>("correctAnswerIndex");

                    var answers = answerJson["answers"].ToObject<List<string>>();

                    JObject solvedExamContentItem = new JObject
                    {
                        new JProperty("type", question.Question.Type),
                        new JProperty("stem", answerJson.Value<string>("stem")),
                        new JProperty("answers", string.Join(", ", answers)),
                        new JProperty("correctAnswer", answers[int.Parse(correctAnswerIndexValue)]),
                        new JProperty("maximumPoints", question.Points),
                        new JProperty("scoredPoints", 0),
                    };

                    if (!string.IsNullOrEmpty(submittedAnswerIndexValue))
                    {
                        solvedExamContentItem["answered"] = answers[int.Parse(submittedAnswerIndexValue)];
                    }
                    else
                    {
                        solvedExamContentItem["answered"] = null;
                    }

                    if (correctAnswerIndexValue == submittedAnswerIndexValue)
                    {
                        solvedExam.ScoredPoints += question.Points;
                        solvedExamContentItem["scoredPoints"] = question.Points;
                    }

                    solvedExamContentList.Add(solvedExamContentItem);
                }
                else if (question.Question.Type.Equals(DefaultApplicationValues.SingleWord))
                {
                    var correctAnswerValues = answerJson["correctAnswers"].ToObject<List<string>>();
                    var submittedAnswerValue = submittedAnswer.Answer;

                    JObject solvedExamContentItem = new JObject
                    {
                        new JProperty("type", question.Question.Type),
                        new JProperty("stem", answerJson.Value<string>("stem")),
                        new JProperty("correctAnswers", string.Join(", ", correctAnswerValues)),
                        new JProperty("answered", submittedAnswerValue),
                        new JProperty("maximumPoints", question.Points),
                        new JProperty("scoredPoints", 0),
                    };

                    if (correctAnswerValues.Contains(submittedAnswerValue))
                    {
                        solvedExam.ScoredPoints += question.Points;
                        solvedExamContentItem["scoredPoints"] = question.Points;
                    }

                    solvedExamContentList.Add(solvedExamContentItem);
                }
                else if (question.Question.Type.Equals(DefaultApplicationValues.MultiChoice))
                {
                    var correctAnswerValues = answerJson["correctAnswersIndexes"].ToObject<List<int>>();
                    var possibleAnswersValues = answerJson["answers"].ToObject<List<string>>();
                    var submittedAnswerValue = submittedAnswer.Answer;
                    var answers = answerJson["answers"].ToObject<List<string>>();

                    var correntAnswersList = new List<string>();

                    foreach (var index in correctAnswerValues)
                    {
                        correntAnswersList.Add(answers[index]);
                    }

                    var submittedAnswerJson = JArray.Parse(submittedAnswerValue);

                    int checkedCorrect = 0;
                    int checkedFalse = 0;
                    List<string> answeredList = new List<string>();

                    for (int i = 0; i < submittedAnswerJson.Count; i++)
                    {
                        var answerValueAtIndex = submittedAnswerJson[i].Value<string>($"{i}");
                        if (answerValueAtIndex.Equals("True", StringComparison.InvariantCultureIgnoreCase))
                        {
                            answeredList.Add(possibleAnswersValues[i]);
                            if (correctAnswerValues.Contains(i))
                            {
                                checkedCorrect++;
                            }
                            else
                            {
                                checkedFalse++;
                            }
                        }
                    }

                    JObject solvedExamContentItem = new JObject
                    {
                        new JProperty("type", question.Question.Type),
                        new JProperty("stem", answerJson.Value<string>("stem")),
                        new JProperty("answers", string.Join(", ", answers)),
                        new JProperty("correctAnswers", string.Join(", ", correntAnswersList)),
                        new JProperty("answered", string.Join(", ", answeredList)),
                        new JProperty("maximumPoints", question.Points),
                        new JProperty("scoredPoints", 0),
                    };

                    if (checkedCorrect > checkedFalse)
                    {
                        var earnedPoints = ((float)question.Points / (float)correctAnswerValues.Count) * ((float)checkedCorrect - (float)checkedFalse);
                        solvedExam.ScoredPoints += earnedPoints;
                        solvedExamContentItem["scoredPoints"] = earnedPoints;
                    }

                    solvedExamContentList.Add(solvedExamContentItem);
                }
            }

            solvedExam.ScoredPercentage = (float)Math.Round((double)solvedExam.ScoredPoints
                / (double)solvedExam.MaximumPoints * 100, 2);

            if (solvedExam.ScoredPercentage >= exam.GradingCriterion.GradeA)
            {
                solvedExam.Grade = 5;
            }
            else if (solvedExam.ScoredPercentage >= exam.GradingCriterion.GradeB)
            {
                solvedExam.Grade = 4;
            }
            else if (solvedExam.ScoredPercentage >= exam.GradingCriterion.GradeC)
            {
                solvedExam.Grade = 3;
            }
            else if (solvedExam.ScoredPercentage >= exam.GradingCriterion.GradeD)
            {
                solvedExam.Grade = 2;
            }
            else
            {
                solvedExam.Grade = 1;
            }

            solvedExamContent["items"] = JArray.FromObject(solvedExamContentList);
            solvedExam.Content = solvedExamContent.ToString(Newtonsoft.Json.Formatting.None);
            var result = await _solvedExamService.CreateSolvedExamAsync(solvedExam);
            return result > 0;
        }

        public async Task<int> UpdateAsync(IExamDto exam)
        {
            _repository.PutAsync(exam);
            return await _uow.SaveAsync();
        }

        public async Task<List<IExamDto>> GetLockedExamsAsync(Guid subjectId)
        {
            var exams = await _repository.GetLockedExamsAsync(subjectId);
            return exams;
        }

        public async Task<List<IExamDto>> GetOpenExamsAsync(Guid subjectId)
        {
            var exams = await _repository.GetOpenExamsAsync(subjectId);
            return exams;
        }

        public async Task<List<IExamDto>> GetFutureExamsAsync(Guid subjectId)
        {
            var exams = await _repository.GetFutureExamsAsync(subjectId);
            return exams;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var exam = new ExamDto();
            exam.Id = id;

            _repository.Delete(exam);
            var result = await _uow.SaveAsync();

            return result > 0;
        }

        public async Task<bool> CheckIfGradingCriteriaIsUsedAsync(Guid criteriaId)
        {
            var examExists = await _repository.CheckIfGradingCriteriaIsUsedAsync(criteriaId);
            return examExists;
        }

        public async Task<int> LockSolvedExamsAsync(Guid examId)
        {
            _repository.LockSolvedExamsAsync(examId);

            var result = await _uow.SaveAsync();
            return result;
        }

        #endregion Methods
    }
}