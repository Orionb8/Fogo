using Fogo.Models;
using Fogo.Services;
using Fogo.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace Fogo.Api.Controllers {

    public class BaseController<TService, TModel, TViewModel> :
        ReadOnlyController<TService, TModel, TViewModel>
        where TService : IService<TModel, TViewModel>
        where TModel : class, new()
        where TViewModel : class, IComparableModel<TModel>, new() {

        public BaseController(
            ILoggerFactory loggerFactory,
            TService service) :
            base(loggerFactory, service) {
        }

        [HttpPost("save")]
        public IActionResult Save([FromBody] TViewModel viewModel, CancellationToken cancellationToken = default) {
            try {
                return Ok(_service.SaveAsync(viewModel, cancellationToken));
            } catch (Exception exception) {
                _logger.LogError(exception, exception.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] TViewModel viewModel, CancellationToken cancellationToken = default) {
            try {
                return Ok(_service.CreateAsync(viewModel, cancellationToken));
            } catch (Exception exception) {
                _logger.LogError(exception, exception.Message);
                if (exception is ConflictException) {
                    return Conflict();
                }
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] TViewModel viewModel, CancellationToken cancellationToken = default) {
            try {
                return Ok(_service.UpdateAsync(viewModel, cancellationToken));
            } catch (Exception exception) {
                _logger.LogError(exception, exception.Message);
                if (exception is NotFoundException) {
                    return NotFound();
                }
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute, Bind("Id")] TViewModel viewModel, CancellationToken cancellationToken = default) {
            try {
                return Ok(_service.UpdateAsync(viewModel, cancellationToken));
            } catch (Exception exception) {
                _logger.LogError(exception, exception.Message);
                if (exception is NotFoundException) {
                    return NotFound();
                }
                return BadRequest();
            }
        }
    }
}