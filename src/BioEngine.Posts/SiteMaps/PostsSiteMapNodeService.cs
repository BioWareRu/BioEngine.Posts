using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BioEngine.Core.Abstractions;
using BioEngine.Core.Site.Sitemaps;
using BioEngine.Posts.Entities;
using cloudscribe.Web.SiteMap;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BioEngine.Posts.SiteMaps
{
    public class PostsSiteMapNodeService : BaseSiteMapNodeService<Post>
    {
        protected override double Priority { get; } = 0.9;

        public PostsSiteMapNodeService(IHttpContextAccessor httpContextAccessor,
            IBioRepository<Post> repository,
            LinkGenerator linkGenerator) :
            base(httpContextAccessor, repository, linkGenerator)
        {
        }

        protected override async Task AddNodesAsync(List<ISiteMapNode> nodes, Post[] entities)
        {
            await base.AddNodesAsync(nodes, entities);
            nodes.Add(new SiteMapNode("/")
            {
                Priority = 1,
                ChangeFrequency = PageChangeFrequency.Daily,
                LastModified = entities.Length > 0
                    ? entities.OrderByDescending(e => e.DateUpdated).First().DateUpdated.DateTime
                    : DateTime.Now
            });
        }
    }
}
