﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WantToSell.Application.Contracts.DataAccess;
using WantToSell.Application.Exceptions;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Features.Category.Validators;

namespace WantToSell.Application.Features.Category.Commands
{
    public static class CreateCategory
    {
        public record Command(CategoryCreateModel model) : IRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IMapper _mapper;
            private readonly ICategoryRepository _categoryRepository;

            public Handler(IMapper mapper, ICategoryRepository categoryRepository)
            {
                _mapper = mapper;
                _categoryRepository = categoryRepository;
            }
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var validator = new CategoryCreateModelValidator();
                    var validationResult = await validator.ValidateAsync(request.model);

                    if (!validationResult.IsValid)
                        throw new BadRequestException("Invalid request!");

                    var entity = _mapper.Map<Domain.Category>(request.model);
                    entity.Id = Guid.NewGuid();

                    await _categoryRepository.CreateAsync(entity);

                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
