using System.Threading.Tasks;
using DockerFunTimes.Features.Fun;
using DockerFunTimes.Infrastructure;
using MediatR;

namespace DockerFunTimes.Features.Prospects
{
    public class ProspectRequestHandler : IAsyncRequestHandler<NewProspectRequest, int>
    {
        private readonly TerribleEntityContext _context;
        private readonly IQueue _queue;

        public ProspectRequestHandler(TerribleEntityContext context, IQueue queue)
        {
            _context = context;
            _queue = queue;
        }

        public async Task<int> Handle(NewProspectRequest message)
        {
            var terribleEntity = new TerribleEntity()
            {
                Name = message.Name,
                DateOfBirth = message.DateOfBirth
            };
            _context.Foo.Add(terribleEntity);

            await _context.SaveChangesAsync();

            _queue.Publish(terribleEntity);

            return terribleEntity.Id;
        }
    }
}