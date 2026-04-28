using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Hook.Application.Contracts.Common;

public class UpdateImagesRequest
{
    public IFormFileCollection? NewImages { get; set; }
    public List<Guid>? ImageIdsToDelete { get; set; }
    public Guid? MainImageId { get; set; }
}
