# In Editor Interactive Tutorials Authoring Tools
---------
This package makes tooling available to be able to create Tutorials.

**This package relies on Unity internals and is not available on production**

## Setup
Follow those steps to enable authoring tools to your project

- Add `com.unity.learn.iet-framework.authoring` to your `dependencies` list to your project `manifest.json`
- Add `com.unity.learn.iet-framework.authoring` to the `testables` list in your `manifest.json`
- point your `manifest.json` to the staging registry

Example:

    {
        "dependencies": {
            "com.unity.learn.iet-framework": "0.1.6-preview",
            "com.unity.learn.iet-framework.authoring": "0.1.2-preview"
        },
        "testables": [
            "com.unity.learn.iet-framework.authoring"
        ],
        "registry": "https://staging-packages.unity.com"
    }
Make sure the framework and the authoring tools have compatible versions, most likely the latest version of each package.

## Authoring tools
This package allows you to create new Assets related to Tutorials:

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