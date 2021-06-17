# About Tutorial Authoring Tools

![](images/hero.png)

This package is used to author interactive in-Editor tutorials (IET) in tutorial projects and project templates. This package provides only UI functionality and no public APIs.

This package should be used while developing tutorials, but the final published tutorial project should not typically include this package as a dependency.

<!-- TODO remember to remove the following for final 1.0.0 release -->
## Preview package
This package is available as a preview package. It is not recommended for use in production. The features and documentation of this package are subject to major changes until its official release.

## Installation
<!-- TODO remember to adjust the following for final 1.0.0 release -->
For Unity 2021.2 and newer, simply search for "Tutorial Authoring Tools" in the Package Manager. For older Unity versions, this package is not currently discoverable,
and you need to add the following line to the `dependencies` list of `Packages/manifest.json`:  
`"com.unity.learn.iet-framework.authoring": "1.0.0-pre.6"`

Make sure the framework and the authoring tools have compatible versions, which are most likely the latest versions of each package.

## Requirements

This version of Tutorial Authoring Tools is compatible with the following versions of the Unity Editor:

* 2019.4 and later (LTS versions recommended)
