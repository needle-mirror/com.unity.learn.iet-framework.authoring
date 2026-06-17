# Tutorial Authoring Tools Upgrade Guide

Tutorial Authoring Tools 2.0.0 doesn't require you to take any action to upgrade from the 1.x version.

## Data format v6 for Tutorial Framework

However, version 6.0.0 of [Tutorial Framework] introduced a big change in how tutorial assets are serialised. As such, when importing any 6.x version of the package for the first time, existing tutorial assets (Tutorial Container, Tutorial Page, Tutorial, etc.) will be automatically migrated to the new version.

When you install Tutorial Authoring Tools 2.0.0, Tutorial Framework 6.0.0 will be automatically installed due to being a dependency, and your tutorial assets will be migrated to the v6 data format.

If you want to trigger a manual migration, go to **Tutorials > Authoring > Upgrade Tutorial Data to v6**.

For more information, go to the [migration guide page](https://docs.unity3d.com/Packages/com.unity.learn.iet-framework@latest/manual/upgrade-guide.html) for Tutorial Framework.

[Tutorial Framework]: https://docs.unity3d.com/Packages/com.unity.learn.iet-framework@latest