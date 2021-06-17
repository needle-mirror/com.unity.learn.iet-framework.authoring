# What's new in version 1.0

Summary of changes in Tutorial Authoring Tools package version 1.0.

The main updates in this release include:

### Added
- Added `SelectGameObject` function to `CommonTutorialCallbacks`.
- Documentation: package documentation/manual added.

### Changed
- UI: Cleaned up and restructured the **Tutorials** menu, authoring-related items can be now found under the **Tutorials** > **Authoring** submenu.
- **Breaking change**: assembly and namespace renamed to `Unity.Tutorials.Authoring.Editor`.
- Updated Tutorial Framework dependency to 2.0.0.

### Removed
- Removed **Tutorials** > **Export Tutorial** and **Tutorials** > **Export all with default settings** menu items.
Tutorial Exporter was experimental and not supported officially.
- **Breaking change**: Removed `CommonTutorialCallbacks` assets. These are moved to Tutorial Framework.

For a full list of changes and updates in this version, see the [Changelog].

[Changelog]: https://docs.unity3d.com/Packages/com.unity.learn.iet-framework.authoring@1.0/changelog/CHANGELOG.html
