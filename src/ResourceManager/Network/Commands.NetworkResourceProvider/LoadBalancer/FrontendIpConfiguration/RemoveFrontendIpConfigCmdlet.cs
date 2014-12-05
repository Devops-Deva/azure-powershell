﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.NetworkResourceProvider.Models;

namespace Microsoft.Azure.Commands.NetworkResourceProvider
{
    [Cmdlet(VerbsCommon.Remove, "AzureLoadBalancerFrontendIpConfig"), OutputType(typeof(PSBackendAddressPool))]
    public class RemoveAzureLoadBalancerFrontendIpConfigCmdlet : NetworkBaseClient
    {
        [Parameter(
            Mandatory = false,
            ValueFromPipeline = true,
            HelpMessage = "The name of the FrontendIp Configuration")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(
             Mandatory = true,
             HelpMessage = "The loadbalancer")]
        public PSLoadBalancer LoadBalancer { get; set; }

        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();

            var frontendipConfiguration = this.LoadBalancer.Properties.FrontendIpConfigurations.SingleOrDefault(resource => string.Equals(resource.Name, this.Name, System.StringComparison.CurrentCultureIgnoreCase));

            if (frontendipConfiguration != null)
            {
                this.LoadBalancer.Properties.FrontendIpConfigurations.Remove(frontendipConfiguration);
            }

            WriteObject(this.LoadBalancer);
        }
    }
}
