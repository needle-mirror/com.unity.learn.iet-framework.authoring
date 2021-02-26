# Tutorial Authoring Tools
---------
This package makes tooling available to be able to create Tutorials.

## Setup
Follow those steps to enable authoring tools to your project

- Add `com.unity.learn.iet-framework.authoring` to your `dependencies` list to your project `manifest.json`

Example:

    {
        "dependencies": {
            "com.unity.learn.iet-framework": "2.0.0-pre",
            "com.unity.learn.iet-framework.authoring": "1.0.0-pre"
        }
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