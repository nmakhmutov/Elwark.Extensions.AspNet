﻿using System;
using System.Threading.Tasks;
using Elwark.Extensions.AspNet.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Elwark.Extensions.AspNet.HttpClientCorrelationId
{
    internal class ElwarkCorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CorrelationIdOptions _options;

        public ElwarkCorrelationIdMiddleware(RequestDelegate next, IOptions<CorrelationIdOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(_options.HeaderName, out var correlationId))
                context.TraceIdentifier = correlationId;

            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add(_options.HeaderName, context.TraceIdentifier);
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}