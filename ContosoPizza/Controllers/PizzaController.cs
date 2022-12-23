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

    /*A ação anterior:

    Responde apenas ao verbo HTTP GET, conforme indicado pelo atributo [HttpGet].
    Consulta o serviço para todas as pizzas e retorna automaticamente os dados
    com um valor Content-Type igual a application/json. */

    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza == null)
            return NotFound();

        return pizza;
    }

    /*A ação anterior:

    Responde apenas ao verbo HTTP GET, conforme indicado pelo atributo [HttpGet].
    Requer que o valor do parâmetro id seja incluído no segmento da URL após pizza/.
    Lembre-se, o atributo [Route] do nível do controlador definiu o padrão /pizza.
    Consulta o banco de dados para uma pizza que corresponde ao parâmetro id fornecido. */


    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Create), new { id = pizza.Id, pizza });
    }

    /*A ação anterior:

    Responde apenas ao verbo HTTP POST, conforme indicado pelo atributo [HttpPost].
    Insere o objeto Pizza do corpo da solicitação no cache na memória.
    Observação

    Como o controlador está anotado com o atributo [ApiController], 
    está implícito que o parâmetro Pizza será encontrado no corpo da solicitação.

    O primeiro parâmetro na chamada de método CreatedAtAction representa um nome de ação.
    A palavra-chave nameof é usada para evitar hard-coding do nome da ação.
    CreatedAtAction usa o nome da ação para gerar um cabeçalho
    de resposta HTTP location com uma URL para a pizza recém-criada,
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
    /*A ação anterior:

    Responde apenas ao verbo HTTP PUT, conforme indicado pelo atributo [HttpPut].
    Requer que o valor do parâmetro id seja incluído no segmento da URL após pizza/.
    Retorna IActionResult porque o tipo de retorno ActionResult não é conhecido até
    o runtime. Os métodos BadRequest, NotFound e NoContent retornam os tipos BadRequestResult, NotFoundResult e
    NoContentResult respectivamente.
    Observação
    Como o controlador está anotado com o atributo [ApiController], está implícito que o parâmetro Pizza será encontrado 
    no corpo da solicitação.*/

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);
        if (pizza is null)
            return NotFound();

        PizzaService.Delete(id);

        return NoContent();
    }

    /*A ação anterior:

    Responde apenas ao verbo HTTP DELETE, conforme indicado pelo atributo [HttpDelete].
    Requer que o valor do parâmetro id seja incluído no segmento da URL após pizza/.
    Retorna IActionResult porque o tipo de retorno ActionResult não é conhecido até o runtime. 
    Os métodos NotFound e NoContent retornam os tipos NotFoundResult e NoContentResult, respectivamente.
    Consulta o cache na memória para uma pizza que corresponde ao parâmetro id fornecido.*/
}