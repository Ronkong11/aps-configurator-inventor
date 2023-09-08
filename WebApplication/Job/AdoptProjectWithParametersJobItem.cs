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

using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApplication.Definitions;
using WebApplication.Services;

namespace WebApplication.Job
{
    internal class AdoptProjectWithParametersJobItem(ILogger logger, ProjectService projectService, string payloadUrl,
        AdoptProjectWithParametersPayloadProvider adoptProjectWithParametersPayloadProvider) : JobItemBase(logger, null, null)
    {
        private readonly ProjectService _projectService = projectService;
        private readonly string _payloadUrl = payloadUrl;
        private readonly AdoptProjectWithParametersPayloadProvider _adoptProjectWithParametersPayloadProvider = adoptProjectWithParametersPayloadProvider;

        public override Task ProcessJobAsync(IResultSender resultSender, object v)
        {
            throw new NotImplementedException();
        }

        internal override object Message()
        {
            throw new NotImplementedException();
        }

        internal override Task ProcessJobAsync(Sender sender, object v)
        {
            throw new NotImplementedException();
        }
    }
}