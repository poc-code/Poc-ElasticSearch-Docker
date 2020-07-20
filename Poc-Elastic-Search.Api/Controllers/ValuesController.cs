using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/values")]
public class ValuesController : ControllerBase
{

    private readonly ILogger<ValuesController> _logger;

    public ValuesController(ILogger<ValuesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get(){
        var value = new
        {
            message = "Retorno do get, com execução do exception.",
            data = ""
        };

        try
        {
            throw new Exception("Ops, não foi possivel prosseguir");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Seu código está bugado");
        }


        return base.Ok(value);
    }
    
    [HttpPost]
    public IActionResult Post(object value){
        return Ok(new {
            message = "Retorno do post",
            data = value
        });
    }

    [HttpPut]
    public IActionResult Put(object value){
        return Ok(new {
            message = "Retorno do put",
            data = value
        });
    }

    [HttpDelete]
    public IActionResult HttpDelete(object value){
        return Ok(new {
            message = "Retorno do delete",
            data = value
        });
    }

}