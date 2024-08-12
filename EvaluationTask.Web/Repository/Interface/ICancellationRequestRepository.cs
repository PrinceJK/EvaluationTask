using EvaluationTask.Web.Models.Dtos;
using EvaluationTask.Web.Models;
using FluentResults;

namespace EvaluationTask.Web.Repository.Interface;

public interface ICancellationRequestRepository
{
    Task CreateCancellationRequest(CancelationRequestDto cancellationRequestDto);
    Task<Result> EditCancellationRequest(CancelationRequestDto cancellationRequestDto);
    Task<Result<CancelationRequestDto>> GetCancellationRequest(int id);
    Task<Result<string>> GetCancellationRequestMessage(int id);
    Task<Result<List<CancelationRequestDto>>> GetAllCancellationRequests();
}
