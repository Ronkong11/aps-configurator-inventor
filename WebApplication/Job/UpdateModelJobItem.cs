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

using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Shared;
using WebApplication.Definitions;
using WebApplication.Processing;

namespace WebApplication.Job
{
    public class UpdateModelJobItem(ILogger logger, string projectId, InventorParameters parameters, ProjectWork projectWork) : JobItemBase(logger, projectId, projectWork)
    {
        public new InventorParameters Parameters { get; }

        public InventorParameters Parameters1 { get; private set; } = parameters;

        public override Task ProcessJobAsync(IResultSender resultSender)
        {
            throw new System.NotImplementedException();
        }
    }
}
