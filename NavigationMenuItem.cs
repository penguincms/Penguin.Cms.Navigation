﻿using Penguin.Cms.Abstractions.Attributes;
using Penguin.Cms.Entities;
using Penguin.Navigation.Abstractions;
using Penguin.Persistence.Abstractions.Attributes.Control;
using Penguin.Persistence.Abstractions.Attributes.Relations;
using Penguin.Shared.Objects.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Penguin.Cms.Navigation
{
    [Table("NavigationMenuItems")]
    public class NavigationMenuItem : AuditableEntity, IRecursiveList<NavigationMenuItem>, INavigationMenu<NavigationMenuItem>
    {
        [EagerLoad(1)]
        [DontAllow(DisplayContexts.Edit | DisplayContexts.BatchEdit | DisplayContexts.List)]
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only")]
        public virtual IList<NavigationMenuItem> Children { get; set; }

        IList<INavigationMenu> INavigationMenu.Children => throw new System.NotImplementedException();
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

        [SuppressMessage("Design", "CA1056:Uri properties should not be strings")]
        public string Uri { get; set; }

        public NavigationMenuItem()
        {
            this.Children = new List<NavigationMenuItem>();
        }

        public NavigationMenuItem(string text, string icon)
        {
            this.Children = new List<NavigationMenuItem>();
            this.Icon = icon;
            this.Text = text;
            this.Name = text;
        }

        public NavigationMenuItem(string text, string href, string icon)
        {
            this.Children = new List<NavigationMenuItem>();
            this.Icon = icon;
            this.Href = href;
            this.Text = text;
            this.Name = text;
        }

        public override string ToString() => $"{this.Uri} - {this.Href}";
    }
}