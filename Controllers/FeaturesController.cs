using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/features")]

public class FeatureController : ControllerBase
{
    private readonly IFeatureService _service;
    public FeatureController(IFeatureService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var features = _service.GetAllFeatures();
        return Ok(features);
    }

    [HttpGet("{id}")]
    public IActionResult GetFeatureById(int id)
    {
        var feature = _service.GetFeatureById(id);
        if (feature == null)
            return NotFound();
        
        return Ok(feature);
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateFeature(int id, [FromBody] bool isEnabled)
    {
        var feature = _service.UpdateFeature(id,isEnabled);
        if (feature == null)
            return NotFound();

        return Ok(feature);
    }

    
    [HttpPost]
    public IActionResult AddFeature(Feature newFeature)
    {
        if (string.IsNullOrWhiteSpace(newFeature.Name))
            return BadRequest();
        
        _service.CreateFeature(newFeature);

        return CreatedAtAction(nameof (GetFeatureById), new {id = newFeature.Id}, newFeature);

        
    }
    
}