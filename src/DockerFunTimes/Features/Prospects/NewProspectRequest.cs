using System;
using MediatR;

namespace DockerFunTimes.Features.Prospects
{
    public class NewProspectRequest: IAsyncRequest<int>
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}