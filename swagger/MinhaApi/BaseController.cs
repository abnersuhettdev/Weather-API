using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/exemplo")]

/// <summary>
/// Controlador de exemplo para demonstrar operações na API.
/// </summary>
public class ExemploController : ControllerBase
{
    /// <summary>
    /// Exemplo de método POST que aceita um objeto de classe como parâmetro.
    /// </summary>
    /// <remarks>
    /// Exemplo de como enviar uma solicitação:
    /// 
    /// (Idade é obrigatória)
    /// 
    ///     POST /api/exemplo/adicionar
    ///     {
    ///        "Nome": "João",
    ///        "Idade": 30 
    ///     }
    /// 
    /// </remarks>
    /// <param name="request">Objeto de classe enviado no corpo da solicitação.</param>
    /// <returns>Objeto de classe que representa a resposta.</returns>
    [HttpPost("adicionar")]
    [ProducesResponseType(201)] // Código 201 para Created
    [ProducesResponseType(400)] // Código 400 para BadRequest
    public IActionResult AdicionarObjeto([FromBody] ExemploRequest request)
    {
        // Lógica para processar o objeto e retornar uma resposta
        if (request == null)
        {
            return BadRequest("Objeto de solicitação inválido.");
        }

        // Lógica adicional aqui...

        return Created("/api/exemplo/adicionar", new ExemploResponse
        {
            Mensagem = "Objeto adicionado com sucesso.",
            Detalhes = "Mais detalhes sobre a operação."
        });
    }
}


/// <summary>
/// Classe que representa a estrutura da resposta do método AdicionarObjeto.
/// </summary>
public class ExemploRequest
{
    /// <summary>
    /// Enviar o nome é obrigatório.
    /// </summary>
    [Required]
    public string Nome { get; set; }
    /// <summary>
    /// Enviar idade é obrigatória.
    /// </summary> 
    [Required] 
    public int Idade { get; set; }
    // Outras propriedades conforme necessário
}

public class ExemploResponse
{
    /// <summary>
    /// Mensagem de sucesso.
    /// </summary>
    public string Mensagem { get; set; }

     /// <summary>
    /// Detalhes adicionais sobre a operação.
    /// </summary>
    public string Detalhes { get; set; }
    // Outras propriedades conforme necessário
}