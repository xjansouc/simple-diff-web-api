using Microsoft.AspNetCore.Mvc;

namespace Diff.WebApi.Controllers;

[ApiController]
[Route("/v1/diff")]
public class DiffController : ControllerBase
{

    private readonly ILogger<DiffController> _logger;
    private readonly IDiffRepository _diff;
    private readonly IDiffComparer _comparer;

    public DiffController(ILogger<DiffController> logger, IDiffRepository diff, IDiffComparer comparer)
    {
        _logger = logger;
        _diff = diff;
        _comparer = comparer;
    }

    [HttpPost("{id}/left")]
    [Consumes("application/custom")]
    public async Task<IActionResult> Left(string id, [FromBody] string input)
    {
        _diff.SetLeft(id, input);

        return Ok();
    }

    [HttpPost("{id}/right")]
    [Consumes("application/custom")]
    public async Task<IActionResult> Right(string id, [FromBody] string input)
    {
        _diff.SetRight(id, input);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<string> Get(string id)
    {
        return _comparer.Compare(id);
    }
}