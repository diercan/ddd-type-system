using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sample.FinanceSystem.Api.Models;
using Sample.FinanceSystem.Domain.Workflows;
using static Sample.FinanceSystem.Domain.Types.InvoiceCreatedEvent;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Controllers;

[ApiController]
[Route("invoice")]
public class InvoiceController : ControllerBase
{
    private readonly CreateInvoiceWorkflow createInvoiceWorkflow;
    //private readonly IInvoiceReadRepository invoiceRepository;
    private readonly IMapper mapper;

    //public InvoiceController(CreateInvoiceWorkflow createInvoiceWorkflow)
    //{
    //    this.createInvoiceWorkflow = createInvoiceWorkflow;
    //}

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(InvoiceDto), 200)]
    [ProducesResponseType(typeof(string), 401)]
    public async Task<ActionResult<InvoiceDto>> Get([FromRoute] int id)
    {
        //return async invoiceRepository.GetAsync(id);
        throw new NotImplementedException();
    }

    [HttpPost]
    [ProducesResponseType(typeof(InvoiceDto), 201)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult<InvoiceDto>> CreateInvoice([FromBody] InvoiceRequest invoiceRequest)
    {
        UnvalidatedInvoice invoice = mapper.Map<UnvalidatedInvoice>(invoiceRequest);
        IInvoiceCreatedEvent invoiceEvent = await createInvoiceWorkflow.RunAsync(invoice);

        return invoiceEvent.Match(
            whenInvoiceCreatedSuccessfulyEvent: created => Created($"invoice/{created.InvoiceId}", mapper.Map<InvoiceDto>(created.Invoice)),
            whenInvoiceCreateFailValidationEvent: failed => BadRequest(mapper.Map<ModelStateDictionary>(failed.Errors)),
            whenInvoiceCreateUnexpectedErrorEvent: error => StatusCode(500, error.ErrorMessage));
    }
}
