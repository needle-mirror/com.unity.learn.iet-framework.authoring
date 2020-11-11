# Tutorial Authoring Tools
---------
This package makes tooling available to be able to create Tutorials.

## Setup
Follow those steps to enable authoring tools to your project

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

## Authoring tools
This package allows you to create new Assets related to Tutorials:

- Tutorial Container
- Tutorial
- Tutorial Page
- Tutorial Styles
- Tutorial Welcome Page
- Tutorial Project Settings

It will also enable the authoring toolbar at the top of the Tutorial Window, which allows the author to:

- Load a specific tutorial
- Re-run start up code
- Disable masking


## More Documentation
Documentation on how to create Tutorials is lacking.
We have this [google doc](https://docs.google.com/document/d/1P3IMwEiNksUp54kholgX2OfFmTRWTfkGwrlKXi2KfVI/edit#heading=h.4a6rzz7g68uy) that needs some cleaning up and updating.
