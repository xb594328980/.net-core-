using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Ocelot.Middleware;
using Ocelot.Middleware.Multiplexer;

namespace OcelotDemo.Aggregator
{
    using OcelotDemo.Dependency;

    public class LeaderAdvancedAggregator : IDefinedAggregator
    {
        public LeaderAdvancedDependency _dependency;

        #region version 13.5.0
        ///// <summary>
        ///// 如果你要使用Ocelot 13.5.0或之前的版本，那请使用这部分的示例代码
        ///// </summary>
        //public class LeaderAdvancedAggregator : IDefinedAggregator
        //{
        //    public LeaderAdvancedDependency _dependency;

        //    public LeaderAdvancedAggregator(LeaderAdvancedDependency dependency)
        //    {
        //        _dependency = dependency;
        //    }
        //    public async Task<DownstreamResponse> Aggregate(List<DownstreamResponse> responses)
        //    {
        //        List<string> results = new List<string>();
        //        var contentBuilder = new StringBuilder();

        //        contentBuilder.Append("{");

        //        foreach (var down in responses)
        //        {
        //            string content = await down.Content.ReadAsStringAsync();
        //            results.Add($"\"{Guid.NewGuid()}\":{content}");
        //        }
        //        contentBuilder.Append(string.Join(",", results));
        //        contentBuilder.Append("}");

        //        var stringContent = new StringContent(contentBuilder.ToString())
        //        {
        //            Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
        //        };

        //        var headers = responses.SelectMany(x => x.Headers).ToList();
        //        return new DownstreamResponse(stringContent, HttpStatusCode.OK, headers, "some reason");
        //    }
        //}
        #endregion version 13.5.0

        #region version 13.5.1
        /// <summary>
        /// 如果你要使用Ocelot 13.5.1或之后的版本，那请使用这部分的示例代码
        /// </summary>
        /// <param name="dependency"></param>
        public LeaderAdvancedAggregator(LeaderAdvancedDependency dependency)
        {
            _dependency = dependency;
        }
        public async Task<DownstreamResponse> Aggregate(List<DownstreamContext> responses)
        {
            List<string> results = new List<string>();
            var contentBuilder = new StringBuilder();

            contentBuilder.Append("{");

            foreach (var down in responses)
            {
                string content = await down.DownstreamResponse.Content.ReadAsStringAsync();
                results.Add($"\"{down.DownstreamReRoute.Key}\":{content}");
            }
            //来自leader的声音
            results.Add($"\"leader\":{{comment:\"我是leader，我组织了他们两个进行调查\"}}");

            contentBuilder.Append(string.Join(",", results));
            contentBuilder.Append("}");

            var stringContent = new StringContent(contentBuilder.ToString())
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            var headers = responses.SelectMany(x => x.DownstreamResponse.Headers).ToList();
            return new DownstreamResponse(stringContent, HttpStatusCode.OK, headers, "some reason");
        }
        #endregion version 13.5.1
    }
}
