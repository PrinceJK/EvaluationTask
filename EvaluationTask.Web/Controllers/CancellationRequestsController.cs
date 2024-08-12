using EvaluationTask.Web.Models.Dtos;
using EvaluationTask.Web.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationTask.Web.Controllers
{
    public class CancellationRequestsController(ICancellationRequestRepository cancellationRequestRepository) : Controller
    {

        // GET: CancellationRequests
        public async Task<IActionResult> Index()
        {
            var result = await cancellationRequestRepository.GetAllCancellationRequests();
            return View(result.Value);
        }

        public async Task<IActionResult> ViewMessage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageResult = await cancellationRequestRepository.GetCancellationRequestMessage(id.Value);

            if (!messageResult.IsSuccess)
            {
                return NotFound();
            }

            return PartialView("_MessageView", messageResult.Value);
        }

        // GET: CancellationRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CancellationRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestorName,RequestDate,Status,FeeAmount,Message")] CancelationRequestDto cancellationRequest)
        {
            if (ModelState.IsValid)
            {
                await cancellationRequestRepository.CreateCancellationRequest(cancellationRequest);
                return RedirectToAction(nameof(Index));
            }
            return View(cancellationRequest);
        }

        // GET: CancellationRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancellationRequest = await cancellationRequestRepository.GetCancellationRequest(id.Value);
            if (!cancellationRequest.IsSuccess)
            {
                return NotFound();
            }
            return View(cancellationRequest.Value);
        }

        // POST: CancellationRequests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestorName,RequestDate,Status,FeeAmount,Message")] CancelationRequestDto cancellationRequest)
        {
            if (id != cancellationRequest.Id)
            {
                return Json(new { success = false, message = "Request not found." });
            }

            if (ModelState.IsValid)
            {
                var cancelationRequestResult = await cancellationRequestRepository.EditCancellationRequest(cancellationRequest);
                if (cancelationRequestResult.IsSuccess)
                {
                    return Json(new { success = true, message = "Request updated successfully" });
                }
                return Json(new { success = false, message = "An error occurred while updating the request." });
            }

            // Collect validation errors
            var errors = ModelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .Select(ms => new
                {
                    Field = ms.Key,
                    Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                }).ToList();

            return Json(new { success = false, message = "Validation failed.", errors = errors });
        }
    }
}
