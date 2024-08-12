using AutoMapper;
using EvaluationTask.Web.Data;
using EvaluationTask.Web.Models;
using EvaluationTask.Web.Models.Dtos;
using EvaluationTask.Web.Models.Enums;
using EvaluationTask.Web.Repository.Interface;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace EvaluationTask.Web.Repository.Implementation;

public class CancellationRequestRepository(ApplicationDbContext Context, ILogger<CancellationRequestRepository> Logger, IMapper Mapper) : ICancellationRequestRepository
{
    public async Task CreateCancellationRequest(CancelationRequestDto cancellationRequestDto)
    {
        try
        {
            var cancelationRequest = Mapper.Map<CancelationRequest>(cancellationRequestDto);

            await Context.AddAsync(cancelationRequest);
            await Context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
        }
    }


    public async Task<Result> EditCancellationRequest(CancelationRequestDto cancellationRequestDto)
    {
        try
        {
            var cancelationRequest = Mapper.Map<CancelationRequest>(cancellationRequestDto);

            if (cancelationRequest.Status != Status.ApprovedWithFee)
            {
                cancelationRequest.FeeAmount = default;
            }

            Context.Update(cancelationRequest);
            await Context.SaveChangesAsync();

            return Result.Ok();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Logger.LogError(ex, "An error occurred while updating the request.");
            return Result.Fail("An error occurred while updating the request.");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return Result.Fail("Request failed please try again");
        }
    }

    public async Task<Result<CancelationRequestDto>> GetCancellationRequest(int id)
    {
        try
        {
            var cancellationRequest = await Context.CancellationRequests.FirstOrDefaultAsync(x => x.Id == id);

            if (cancellationRequest == null)
                return Result.Fail("Not found");

            var mappedResult = Mapper.Map<CancelationRequestDto>(cancellationRequest);

            return Result.Ok(mappedResult);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return Result.Fail("Request failed please try again");
        }
    }

    public async Task<Result<string>> GetCancellationRequestMessage(int id)
    {
        try
        {
            var message = await Context.CancellationRequests
                .Where(m => m.Id == id)
                .Select(m => m.Message)
                .FirstOrDefaultAsync();

            if (message == null)
                return Result.Fail("Not found");

            return Result.Ok(message);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return Result.Fail("Request failed please try again");
        }
    }

    public async Task<Result<List<CancelationRequestDto>>> GetAllCancellationRequests()
    {
        try
        {
            var cancellationRequests = await Context.CancellationRequests.ToListAsync();

            var result = Mapper.Map<List<CancelationRequestDto>>(cancellationRequests);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return Result.Fail("Request failed please try again");
        }
    }

    private bool CancellationRequestExists(int id)
    {
        return Context.CancellationRequests.Any(e => e.Id == id);
    }
}
