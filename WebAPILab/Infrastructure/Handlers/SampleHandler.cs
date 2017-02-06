using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebAPILab.Infrastructure.Handlers
{
    internal class SampleHandler : DelegatingHandler
    {
        private string _text;
        private string _indentation;

        public SampleHandler(string text, int indent)
        {
            if (text == null) { throw new ArgumentNullException("text"); }
            if (indent < 0) { throw new ArgumentException("indent"); }
            _text = text;
            _indentation = new String(' ', indent);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("{0}{1} request path", _indentation, _text);

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Debug.WriteLine("{0}{1} response path", _indentation, _text);

            return response;
        }
    }
}