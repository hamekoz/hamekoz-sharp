﻿//
//  SearchDialog.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2016 Hamekoz - www.hamekoz.com.ar
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using Hamekoz.Core;
using Hamekoz.Extensions;
using Xwt;
using Xwt.Drawing;

namespace Hamekoz.UI
{
	public abstract class SearchDialog<T> : Dialog, ISearchDialog<T> where T : ISearchable
	{
		protected bool Reset {
			get;
			set;
		}

		#region ISearchDialog implementation

		public virtual void Refresh ()
		{
			if (!Reset)
				FiltrarLista ();
		}

		public T SelectedItem {
			get;
			private set;
		}

		public IList<T> List {
			get;
			set;
		}

		#endregion

		readonly SearchTextEntry search = new SearchTextEntry {
			TooltipText = Application.TranslationCatalog.GetString ("Tip: Try use field search. Ex: Status=Active")
		};

		readonly protected ListView listView = new ListView {
			GridLinesVisible = GridLines.Both,
			HeightRequest = 200
		};

		readonly protected HBox actionBox = new HBox ();

		readonly Button refresh = new Button {
			Label = Application.TranslationCatalog.GetString ("_Refresh"),
			Image = Icons.ViewRefresh.WithSize (IconSize.Small),
			TooltipText = Application.TranslationCatalog.GetString ("Refresh the list including newest items"),
			UseMnemonic = true
		};

		readonly Button clearFilter = new Button {
			Label = Application.TranslationCatalog.GetString ("_Clear filters"),
			Image = Icons.EditClearAll.WithSize (IconSize.Small),
			UseMnemonic = true
		};

		protected DataField<T> itemDataField = new DataField<T> ();

		protected IController<T> Controller {
			get;
			set;
		}

		protected ListStore store;
		protected IList<T> listFiltered;

		protected VBox filtersBox = new VBox ();

		protected Label result = new Label {
			HorizontalPlacement = WidgetPlacement.Center
		};

		public string Info {
			get;
			set;
		}

		protected SearchDialog (IController<T> controller)
		{
			Controller = controller;
			Title = Application.TranslationCatalog.GetString ("Search");
			search.SetFocus ();
			search.SetCompletions (typeof(T).GetPropertiesNames ());

			refresh.Clicked += delegate {
				search.Text = string.Empty;
				if (Controller != null)
					Controller.Reload = true;
				Refresh ();
			};

			clearFilter.Clicked += (sender, e) => ResetFilter_Clicked ();

			search.Activated += delegate {
				search.BackgroundColor = search.Text == string.Empty ? Colors.White : Colors.LightGreen;
				Refresh ();
			};

			search.Changed += delegate {
				search.BackgroundColor = Colors.White;
				if (search.Text == string.Empty)
					Refresh ();
			};

			listView.RowActivated += delegate {
				var current = listView.SelectedRow;
				SelectedItem = store.GetValue (current, itemDataField);
				Respond (Command.Ok);
			};

			actionBox.PackStart (refresh, true);
			actionBox.PackStart (clearFilter, true);

			var dialogoVBox = new VBox ();

			dialogoVBox.PackStart (new Label (Application.TranslationCatalog.GetString ("Text search")));
			dialogoVBox.PackStart (search);
			dialogoVBox.PackStart (filtersBox);
			dialogoVBox.PackStart (listView, true, true);
			dialogoVBox.PackStart (result);
			dialogoVBox.PackEnd (actionBox);

			Width = 700;
			Content = dialogoVBox;
		}

		void FiltrarLista ()
		{
			listFiltered = List ?? Controller.List;

			if (search.Text != string.Empty)
				listFiltered = listFiltered.Where (r => r.ToSearchString ().ToUpperInvariant ().Contains (search.Text.ToUpperInvariant ())).ToList ();

			store.Clear ();
			FillStore ();

			search.BackgroundColor = search.Text == string.Empty ? Colors.White : store.RowCount == 0 ? Colors.Red : Colors.LightGreen;
			result.Text = string.Format (Application.TranslationCatalog.GetPluralString ("{0} result {1}", "{0} results {1}", store.RowCount), store.RowCount, Info);
		}

		protected virtual void FillStore ()
		{

		}

		protected virtual void ResetFilter_Clicked ()
		{
			search.Text = string.Empty;
			search.BackgroundColor = Colors.White;
			Reset = false;
			Refresh ();
		}
	}
}

