﻿using CryptLearn.Modules.ModuleManagement.Core.DTOs;
using CryptLearn.Shared.Abstractions.Cqrs;

namespace CryptLearn.Modules.ModuleManagement.Core.Queries;
internal record GetModules() : IQuery<IEnumerable<ModuleListItemDto>>;
