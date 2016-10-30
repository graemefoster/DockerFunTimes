using System.Threading.Tasks;
using DockerFunTimes.Features.Fun;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DockerFunTimes.Features.Prospects
{
    public class GetProspectRequest: IAsyncRequest<TerribleEntity>
    {
        public int Id { get; private set; }

        public GetProspectRequest(int id)
        {
            Id = id;
        }
    }

    public class GetProspectRequestHandler: IAsyncRequestHandler<GetProspectRequest, TerribleEntity> {
        private readonly TerribleEntityContext _context;

        public GetProspectRequestHandler(TerribleEntityContext context)
        {
            _context = context;
        }

        public Task<TerribleEntity> Handle(GetProspectRequest message)
        {
            return _context.Foo.SingleOrDefaultAsync(x => x.Id == message.Id);
        }
    }
}