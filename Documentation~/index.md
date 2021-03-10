# About Tutorial Authoring Tools

![](images/hero.png)

Use the Tutorial Authoring tools to author In-Editor Tutorials. Currently, this package provides only UI functionality and no public APIs.

The Tutorial Authoring Tools should be used while developing tutorials, but the final published tutorial projects should not include the Tutorial Authoring tools.

## Preview package
This package is available as a preview package. It is not recommended for use in production. The features and documentation of this package are subject to major changes until its official release.

## Package contents

The following table describes the package folder structure:

|**Location**|**Description**|
|---|---|
|Editor|Contains the code of the package that can be used in Edit Mode.|
|Tests|Contains the tests of the package.|

## Installation

Add `com.unity.learn.iet-framework.authoring` to the `dependencies` list in your project's `manifest.json`.

Make sure the framework and the authoring tools have compatible versions, which are most likely the latest versions of each package.

## Requirements

This version of Tutorial Authoring Tools is compatible with the following versions of the Unity Editor:

* 2019.4 (recommended) and later
