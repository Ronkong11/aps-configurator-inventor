/////////////////////////////////////////////////////////////////////
// Copyright (c) Autodesk, Inc. All rights reserved
// Written by Autodesk Design Automation team for Inventor
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
/////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.Threading.Tasks;
using WebApplication.Definitions;
using WebApplication.Processing;

namespace WebApplication.Job
{
    public abstract class JobItemBase
    {
        protected ILogger Logger { get; }
        protected ProjectWork ProjectWork { get; }
        public string ProjectId { get; }
        public string Id { get; }
        public InventorParameters Parameters { get; private set; }
        public object payloadURL { get; private set; }

        protected JobItemBase(ILogger logger, string projectId, ProjectWork projectWork)
        {
            ProjectId = projectId;
            Id = Guid.NewGuid().ToString();
            ProjectWork = projectWork;
            Logger = logger;
        }

        public abstract Task ProcessJobAsync(
            IResultSender resultSender, object v);
        internal abstract object Message();
        internal abstract Task ProcessJobAsync(Sender sender, object v);

        public override async Task ProcessJobAsync(IResultSender resultSender)
        {
            using IDisposable scope = Logger.BeginScope("Project Adoption ({Id})");

            var payload = await _adoptProjectWithParametersPayloadProvider.GetParametersAsync(payloadURL);

                Logger.LogInformation($"ProcessJob (AdoptProjectWithParameters) {Id} for project {payload.Name} started.");

                var projectWithParameters = await _projectService.AdoptProjectWithParametersAsync(payload);

                Logger.LogInformation($"ProcessJob (AdoptProjectWithParameters) {Id} for project {payload.Name} completed.");

                await resultSender.SendSuccessAsync(projectWithParameters);
        }
    }

    internal class _projectService
    {
        internal static Task AdoptProjectWithParametersAsync(object payload)
        {
            throw new NotImplementedException();
        }
    }

    internal class _adoptProjectWithParametersPayloadProvider
    {
        internal static Task GetParametersAsync(object payloadUrl)
        {
            throw new NotImplementedException();
        }
    }
}
