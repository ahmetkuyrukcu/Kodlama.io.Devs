﻿using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using MediatR;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;

public class ProgrammingLanguageCommand : IRequest<ProgrammingLanguageDto>
{
    public string Name { get; set; }
}