using MagniseTask.Data;
using MagniseTask.Data.Dtos;
using MagniseTask.Data.Models;
using MagniseTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace MagniseTask.Controllers;

[ApiController]
[Route ("[controller]")]
public class PriceController : ControllerBase
{
    private readonly IInstrumentRepository _instrumentRepository;
    private readonly IHistoricalDataRequestService _histDataRequestService;
    private readonly IRealTimeService _realTimeService;

    public PriceController (IInstrumentRepository instrumentRepository, IHistoricalDataRequestService histDataRequestService, IRealTimeService realTimeService)
    {
        _instrumentRepository = instrumentRepository;
        _histDataRequestService = histDataRequestService;
        _realTimeService = realTimeService;
    }

    [HttpGet ("all")]
    public async Task<ActionResult<IEnumerable<InstrumentDto>>> GetAllInstrumentsEndpoint ()
    {
        var instruments = await _instrumentRepository.GetAllInstruments ();
        return Ok (instruments.Select (x => new InstrumentDto (x.Symbol, x.Kind, x.Description, x.TickSize, x.Currency, x.BaseCurrency)));
    }

    [HttpGet ("countBack")]
    public async Task<ActionResult<IEnumerable<CandleDto>>> GetCountBackEndpoint (string symbol, int interval, string periodicity, int barsCount)
    {
        var instrumentId = await _instrumentRepository.GetInstrumentIdAsync (symbol);
        if ( instrumentId is not null )
        {
            try
            {
                var result = await _histDataRequestService.RequestCountBack (instrumentId, interval, periodicity, barsCount);
                return Ok (result);
            }
            catch ( HttpRequestException ex )
            {
                return BadRequest (ex.HttpRequestError);
            }
        }
        return NotFound ();
    }

    [HttpPost ("subscribe")]
    public async Task<ActionResult> Subscribe (string symbol)
    {
        var instrumentId = await _instrumentRepository.GetInstrumentIdAsync (symbol);
        if ( instrumentId is not null )
        {
            try
            {
                await _realTimeService.Subscribe (symbol, instrumentId);
                return Ok ();
            }
            catch ( HttpRequestException ex )
            {
                return BadRequest (ex.HttpRequestError);
            }
        }
        return NotFound ();
    }
}