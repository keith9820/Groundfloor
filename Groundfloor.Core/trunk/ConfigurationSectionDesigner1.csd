<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="54bd7d92-4a36-48ee-b69a-5f3c58b073cc" namespace="Groundfloor" xmlSchemaNamespace="urn:Groundfloor" assemblyName="Groundfloor" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="AppServicesSection" namespace="Groundfloor.Configuration.AppServices" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="appServicesSection">
      <attributeProperties>
        <attributeProperty name="enabled" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="enabled" isReadOnly="false" defaultValue="true">
          <type>
            <externalTypeMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="requestTTLSeconds" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="requestTTLSeconds" isReadOnly="false" defaultValue="3600">
          <type>
            <externalTypeMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="secret" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="secret" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="ipSecurity" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="ipSecurity" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/ipSecurityElement" />
          </type>
        </elementProperty>
        <elementProperty name="commands" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="commands" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/CommandElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="HostElement" namespace="Groundfloor.Configuration.AppServices">
      <attributeProperties>
        <attributeProperty name="host" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="host" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="CommandElementCollection" namespace="Groundfloor.Configuration.AppServices" xmlItemName="commands" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/CommandElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="CommandElement" namespace="Groundfloor.Configuration.AppServices">
      <attributeProperties>
        <attributeProperty name="method" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="method" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="enabled" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="enabled" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="type" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="type" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="description" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="description" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="ipSecurityElement" namespace="Groundfloor.Configuration.AppServices">
      <elementProperties>
        <elementProperty name="allow" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="allow" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/AllowCollection" />
          </type>
        </elementProperty>
        <elementProperty name="deny" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="deny" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/DenyCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="AllowCollection" namespace="Groundfloor.Configuration.AppServices" xmlItemName="deny" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/HostElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="DenyCollection" namespace="Groundfloor.Configuration.AppServices" xmlItemName="allow" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/54bd7d92-4a36-48ee-b69a-5f3c58b073cc/HostElement" />
      </itemType>
    </configurationElementCollection>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>