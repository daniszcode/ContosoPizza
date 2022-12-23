using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }

    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
    PizzaService.GetAll();

    /*A a��o anterior:

    Responde apenas ao verbo HTTP GET, conforme indicado pelo atributo [HttpGet].
    Consulta o servi�o para todas as pizzas e retorna automaticamente os dados
    com um valor Content-Type igual a application/json. */

    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza == null)
            return NotFound();

        return pizza;
    }

    /*A a��o anterior:

    Responde apenas ao verbo HTTP GET, conforme indicado pelo atributo [HttpGet].
    Requer que o valor do par�metro id seja inclu�do no segmento da URL ap�s pizza/.
    Lembre-se, o atributo [Route] do n�vel do controlador definiu o padr�o /pizza.
    Consulta o banco de dados para uma pizza que corresponde ao par�metro id fornecido. */


    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Create), new { id = pizza.Id, pizza });
    }

    /*A a��o anterior:

    Responde apenas ao verbo HTTP POST, conforme indicado pelo atributo [HttpPost].
    Insere o objeto Pizza do corpo da solicita��o no cache na mem�ria.
    Observa��o

    Como o controlador est� anotado com o atributo [ApiController], 
    est� impl�cito que o par�metro Pizza ser� encontrado no corpo da solicita��o.

    O primeiro par�metro na chamada de m�todo CreatedAtAction representa um nome de a��o.
    A palavra-chave nameof � usada para evitar hard-coding do nome da a��o.
    CreatedAtAction usa o nome da a��o para gerar um cabe�alho
    de resposta HTTP location com uma URL para a pizza rec�m-criada,
    conforme explicado na unidade anterior.*/

    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();


        var existePizza = PizzaService.Get(id);
        if (existePizza is null)
            return NotFound();


        PizzaService.Update(pizza);

        return NoContent();
    }
    /*A a��o anterior:

    Responde apenas ao verbo HTTP PUT, conforme indicado pelo atributo [HttpPut].
    Requer que o valor do par�metro id seja inclu�do no segmento da URL ap�s pizza/.
    Retorna IActionResult porque o tipo de retorno ActionResult n�o � conhecido at�
    o runtime. Os m�todos BadRequest, NotFound e NoContent retornam os tipos BadRequestResult, NotFoundResult e
    NoContentResult respectivamente.
    Observa��o
    Como o controlador est� anotado com o atributo [ApiController], est� impl�cito que o par�metro Pizza ser� encontrado 
    no corpo da solicita��o.*/

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);
        if (pizza is null)
            return NotFound();

        PizzaService.Delete(id);

        return NoContent();
    }

    /*A a��o anterior:

    Responde apenas ao verbo HTTP DELETE, conforme indicado pelo atributo [HttpDelete].
    Requer que o valor do par�metro id seja inclu�do no segmento da URL ap�s pizza/.
    Retorna IActionResult porque o tipo de retorno ActionResult n�o � conhecido at� o runtime. 
    Os m�todos NotFound e NoContent retornam os tipos NotFoundResult e NoContentResult, respectivamente.
    Consulta o cache na mem�ria para uma pizza que corresponde ao par�metro id fornecido.*/
}