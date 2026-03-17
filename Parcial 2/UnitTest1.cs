namespace Parcial_2
{
    public class Tests
    {
        private static IEnumerable<TestCaseData> CreacionDeArticulosData()
        {
            yield return new TestCaseData("Espada", 10, "Weapon", true).SetName("caso_1_articulo_weapon_valido");
            yield return new TestCaseData("Casco", 20, "Armor", true).SetName("caso_1_articulo_armor_valido");
            yield return new TestCaseData("Anillo", 15, "Accessory", true).SetName("caso_1_articulo_accessory_valido");
            yield return new TestCaseData("Pocion", 5, "Supply", true).SetName("caso_1_articulo_supply_valido");
            yield return new TestCaseData("", 10, "Weapon", false).SetName("caso_1_articulo_nombre_vacio");
            yield return new TestCaseData("Espada", 0, "Weapon", false).SetName("caso_1_articulo_precio_cero");
            yield return new TestCaseData("Espada", -2, "Weapon", false).SetName("caso_1_articulo_precio_negativo");
            yield return new TestCaseData("Espada", 10, "Food", false).SetName("caso_1_articulo_categoria_invalida");
        }

        [TestCaseSource(nameof(CreacionDeArticulosData))]
        public void caso_1_creacion_de_articulos(string name, int price, string category, bool expected)
        {
            Item item = new Item(name, price, category);

            Assert.That(item.IsValid(), Is.EqualTo(expected));
        }


        private static IEnumerable<TestCaseData> CreacionDeTiendasData()
        {
            yield return new TestCaseData(new Item("Espada", 10, "Weapon"), 2, true).SetName("caso_2_tienda_valida_con_stock");
            yield return new TestCaseData(new Item("Pocion", 5, "Supply"), 1, true).SetName("caso_2_tienda_valida_supply");
            yield return new TestCaseData(new Item("Espada", 10, "Weapon"), 0, false).SetName("caso_2_tienda_sin_stock");
            yield return new TestCaseData(new Item("", 10, "Weapon"), 2, false).SetName("caso_2_tienda_con_item_invalido");
        }

        [TestCaseSource(nameof(CreacionDeTiendasData))]
        public void caso_2_creacion_de_tiendas(Item item, int quantity, bool expectedHasItems)
        {
            Store store = new Store(item, quantity);

            Assert.That(store.HasItemsForSale(), Is.EqualTo(expectedHasItems));
        }

        private static IEnumerable<TestCaseData> SurtirTiendasData()
        {
            yield return new TestCaseData(1, 1, 2).SetName("caso_2_surtir_item_existente_1");
            yield return new TestCaseData(2, 3, 5).SetName("caso_2_surtir_item_existente_2");
            yield return new TestCaseData(5, 0, 5).SetName("caso_2_surtir_item_existente_3");
        }

        [TestCaseSource(nameof(SurtirTiendasData))]
        public void caso_2_surtir_tienda_actualiza_cantidad_si_el_item_ya_existe(int initialQuantity, int addedQuantity, int expectedQuantity)
        {
            Item sword = new Item("Espada", 10, "Weapon");
            Store store = new Store(sword, initialQuantity);

            bool result = store.AddItem(new Item("Espada", 10, "Weapon"), addedQuantity);

            Assert.That(result, Is.True);
            Assert.That(store.GetQuantity("Espada", "Weapon"), Is.EqualTo(expectedQuantity));
        }

        private static IEnumerable<TestCaseData> PrecioDiferenteData()
        {
            yield return new TestCaseData(20).SetName("caso_2_precio_diferente_1");
            yield return new TestCaseData(30).SetName("caso_2_precio_diferente_2");
        }

        [TestCaseSource(nameof(PrecioDiferenteData))]
        public void caso_2_tienda_no_acepta_mismo_nombre_y_categoria_con_precio_diferente(int newPrice)
        {
            Item sword = new Item("Espada", 10, "Weapon");
            Store store = new Store(sword, 2);

            bool result = store.AddItem(new Item("Espada", newPrice, "Weapon"), 1);

            Assert.That(result, Is.False);
            Assert.That(store.GetQuantity("Espada", "Weapon"), Is.EqualTo(2));
        }

        private static IEnumerable<TestCaseData> MismoNombreCategoriaDistintaData()
        {
            yield return new TestCaseData("Weapon", "Accessory").SetName("caso_2_mismo_nombre_categoria_distinta_1");
            yield return new TestCaseData("Armor", "Supply").SetName("caso_2_mismo_nombre_categoria_distinta_2");
        }

        [TestCaseSource(nameof(MismoNombreCategoriaDistintaData))]
        public void caso_2_tienda_si_acepta_mismo_nombre_si_la_categoria_es_distinta(string firstCategory, string secondCategory)
        {
            Item firstItem = new Item("Orbe", 10, firstCategory);
            Item secondItem = new Item("Orbe", 20, secondCategory);

            Store store = new Store(firstItem, 1);
            bool result = store.AddItem(secondItem, 2);

            Assert.That(result, Is.True);
            Assert.That(store.GetQuantity("Orbe", firstCategory), Is.EqualTo(1));
            Assert.That(store.GetQuantity("Orbe", secondCategory), Is.EqualTo(2));
        }

        private static IEnumerable<TestCaseData> CreacionDePersonajesData()
        {
            yield return new TestCaseData(100, 100).SetName("caso_3_personaje_oro_100");
            yield return new TestCaseData(0, 0).SetName("caso_3_personaje_oro_0");
            yield return new TestCaseData(-10, 0).SetName("caso_3_personaje_oro_negativo");
        }

        [TestCaseSource(nameof(CreacionDePersonajesData))]
        public void caso_3_creacion_de_personajes(int inputGold, int expectedGold)
        {
            Player player = new Player(inputGold);

            Assert.That(player.Gold, Is.EqualTo(expectedGold));
            Assert.That(player.Inventory.EquipmentCount, Is.EqualTo(0));
            Assert.That(player.Inventory.SupplyCount, Is.EqualTo(0));
        }
    }
}