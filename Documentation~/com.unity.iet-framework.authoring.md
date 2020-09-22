# About Tutorial Authoring Tools

Use the Tutorial Authoring tools to author In-Editor Tutorials. Currently this package provides only UI functionality and no public APIs.

The Tutorial Authoring tools should be used while developing tutorials, but the final published tutorial projects should not depend on it.

## Preview package
This package is available as a preview package, meaning it is not recommended for production use. The features and documentation of this package are subjects for major changes until it is released officially.

## Package contents

The following table describes the package folder structure:

|**Location**|**Description**|
|---|---|
|Editor|Contains the code of the package that can be used in Edit Mode.|
|Tests|Contains the tests of the package.|
|UserGuide|Contains additional helpful resources, such as examples and local documentation.|

## Installation

Follow those steps to enable authoring tools to your project:

- Add `com.unity.learn.iet-framework.authoring` to your `dependencies` list to your project `manifest.json`
- point your `manifest.json` to the candidates registry

Example:

    {
        "dependencies": {
            "com.unity.learn.iet-framework": "1.1.0",
            "com.unity.learn.iet-framework.authoring": "0.6.3-preview"
        },
        "registry": "https://artifactory.prd.cds.internal.unity3d.com/artifactory/api/npm/upm-candidates"
    }
Make sure the Framework and the Authoring Tools have compatible versions, most likely the latest version of each package.

You can also refer to the "Adding the IET Framework" section of the "IET 2020 Tutorial Authoring Cookbook" file included in the `UserGuide` folder.

## Requirements

This version of Tutorial Authoring Tools is compatible with the following versions of the Unity Editor:

* 2019.4 (recommended) and later

# Using Tutorial Authoring Tools

See the instructions described in the "IET 2020 Tutorial Authoring Cookbook" file included in the `UserGuide` folder.
