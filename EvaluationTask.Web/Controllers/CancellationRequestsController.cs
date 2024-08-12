using EvaluationTask.Web.Data;
using EvaluationTask.Web.Models;
using EvaluationTask.Web.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluationTask.Web.Controllers
{
    public class CancellationRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CancellationRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CancellationRequests
        public async Task<IActionResult> Index()
        {
            return View(await _context.CancellationRequests.ToListAsync());
        }

        public async Task<IActionResult> ViewMessage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.CancellationRequests
                .Where(m => m.Id == id)
                .Select(m => m.Message)
                .FirstOrDefaultAsync();

            if (message == null)
            {
                return NotFound();
            }

            return PartialView("_MessageView", message);
        }

        // GET: CancellationRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CancellationRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestorName,RequestDate,Status,FeeAmount,Message")] CancellationRequest cancellationRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cancellationRequest);
                await _context.SaveChangesAsync();
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

            var cancellationRequest = await _context.CancellationRequests.FindAsync(id);
            if (cancellationRequest == null)
            {
                return NotFound();
            }
            return View(cancellationRequest);
        }

        // POST: CancellationRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestorName,RequestDate,Status,FeeAmount,Message")] CancellationRequest cancellationRequest)
        {
            if (id != cancellationRequest.Id)
            {
                return Json(new { success = false, message = "Request not found." });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (cancellationRequest.Status != Status.ApprovedWithFee)
                    {
                        cancellationRequest.FeeAmount = default(decimal);
                    }
                    _context.Update(cancellationRequest);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Request updated successfully." });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CancellationRequestExists(cancellationRequest.Id))
                    {
                        return Json(new { success = false, message = "Request not found." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "An error occurred while updating the request." });
                    }
                }
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

        private bool CancellationRequestExists(int id)
        {
            return _context.CancellationRequests.Any(e => e.Id == id);
        }
    }
}
