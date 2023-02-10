using Penguin.Cms.Abstractions.Attributes;
using Penguin.Cms.Entities;
using Penguin.Navigation.Abstractions;
using Penguin.Persistence.Abstractions.Attributes.Control;
using Penguin.Persistence.Abstractions.Attributes.Relations;
using Penguin.Shared.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Penguin.Cms.Navigation
{
    [Table("NavigationMenuItems")]
    public class NavigationMenuItem : AuditableEntity, IRecursiveList<NavigationMenuItem>, INavigationMenu<NavigationMenuItem>
    {
        [EagerLoad(1)]
        [DontAllow(DisplayContexts.Edit | DisplayContexts.BatchEdit | DisplayContexts.List)]
        public virtual IList<NavigationMenuItem> Children { get; set; }

        IList<INavigationMenu> INavigationMenu.Children => Children.Cast<INavigationMenu>().ToList();

        public string Href { get; set; }

        [CustomRoute(DisplayContexts.Edit, "Edit", "MaterialIconSelector", "Admin")]
        public string Icon { get; set; }

        public string Name { get; set; }

        public int Ordinal { get; set; }

        [EagerLoad(1)]
        [OptionalToMany]
        [DontAllow(DisplayContexts.Edit | DisplayContexts.BatchEdit | DisplayContexts.View)]
        public NavigationMenuItem Parent { get; set; }

        INavigationMenu INavigationMenu.Parent => Parent;

        public string Text { get; set; }

        public string Uri { get; set; }

        public NavigationMenuItem()
        {
            Children = new List<NavigationMenuItem>();
        }

        public NavigationMenuItem(string text, string icon)
        {
            Children = new List<NavigationMenuItem>();
            Icon = icon;
            Text = text;
            Name = text;
        }

        public NavigationMenuItem(string text, string href, string icon)
        {
            Children = new List<NavigationMenuItem>();
            Icon = icon;
            Href = href;
            Text = text;
            Name = text;
        }

        public override string ToString()
        {
            return $"{Uri} - {Href}";
        }
    }
}