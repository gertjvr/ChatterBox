<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="ChatterBox.Server.Azure" generation="1" functional="0" release="0" Id="2d699018-e50d-4113-adef-bc51dd803c83" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="ChatterBox.Server.AzureGroup" generation="1" functional="0" release="0">
      <settings>
        <aCS name="ChatterBox.Server.CloudService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ChatterBox.Server.Azure/ChatterBox.Server.AzureGroup/MapChatterBox.Server.CloudService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="ChatterBox.Server.CloudServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/ChatterBox.Server.Azure/ChatterBox.Server.AzureGroup/MapChatterBox.Server.CloudServiceInstances" />
          </maps>
        </aCS>
      </settings>
      <maps>
        <map name="MapChatterBox.Server.CloudService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ChatterBox.Server.Azure/ChatterBox.Server.AzureGroup/ChatterBox.Server.CloudService/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapChatterBox.Server.CloudServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/ChatterBox.Server.Azure/ChatterBox.Server.AzureGroup/ChatterBox.Server.CloudServiceInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="ChatterBox.Server.CloudService" generation="1" functional="0" release="0" software="C:\Users\gertjvr\Documents\GitHub\ChatterBox\src\ChatterBox.Server.WorkerRole\csx\Debug\roles\ChatterBox.Server.CloudService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;ChatterBox.Server.CloudService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;ChatterBox.Server.CloudService&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/ChatterBox.Server.Azure/ChatterBox.Server.AzureGroup/ChatterBox.Server.CloudServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/ChatterBox.Server.Azure/ChatterBox.Server.AzureGroup/ChatterBox.Server.CloudServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/ChatterBox.Server.Azure/ChatterBox.Server.AzureGroup/ChatterBox.Server.CloudServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="ChatterBox.Server.CloudServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="ChatterBox.Server.CloudServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="ChatterBox.Server.CloudServiceInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
</serviceModel>