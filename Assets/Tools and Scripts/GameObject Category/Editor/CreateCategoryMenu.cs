using UnityEngine;
using UnityEditor;

static class CreateCategoryMenu
{
    const string MenuItemPrefix = "GameObject/Category/";
    const int BasePriority = -100_000;

    const string CategoryNameFormat = "=== {0:U}";

    [MenuItem(MenuItemPrefix + "Model",         priority = BasePriority)]
    static void CreateModel() => CreateOfName("Model");
    [MenuItem(MenuItemPrefix + "View",          priority = BasePriority + 5)]
    static void CreateView() => CreateOfName("View");
    [MenuItem(MenuItemPrefix + "Controller",    priority = BasePriority + 10)]
    static void CreateController() => CreateOfName("Controller");

    [MenuItem(MenuItemPrefix + "Managers", priority = BasePriority + 100)]
    static void CreateManagers() => CreateOfName("Managers");
    [MenuItem(MenuItemPrefix + "Level",         priority = BasePriority + 105)]
    static void CreateLevel() => CreateOfName("Level");
    [MenuItem(MenuItemPrefix + "Entities",      priority = BasePriority + 110)]
    static void CreateEntities() => CreateOfName("Entities");
    [MenuItem(MenuItemPrefix + "Logic",         priority = BasePriority + 115)]
    static void CreateLogic() => CreateOfName("Logic");
    [MenuItem(MenuItemPrefix + "UI",            priority = BasePriority + 120)]
    static void CreateUI() => CreateOfName("UI");

    [MenuItem(MenuItemPrefix + "Other",         priority = BasePriority + 500)]
    static void CreateOther() => CreateOfName("Other");

    [MenuItem(MenuItemPrefix + "Temp", priority = BasePriority + 1000)]
    static void CreateTemp() => CreateOfName("Temp");
    [MenuItem(MenuItemPrefix + "Old", priority = BasePriority + 1005)]
    static void CreateOld() => CreateOfName("Old");

    static void CreateOfName(string name)
    {
        GameObject go = new GameObject(string.Format(CategoryNameFormat, name.ToUpper()));
        go.transform.position = Vector3.zero;
        go.transform.eulerAngles = Vector3.zero;
        go.transform.localScale = Vector3.one;

        Undo.RegisterCreatedObjectUndo(go, name);

        EditorApplication.delayCall += () => Selection.activeGameObject = go;
    }
}