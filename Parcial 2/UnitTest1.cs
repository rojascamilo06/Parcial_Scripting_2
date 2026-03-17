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

        private static IEnumerable<TestCaseData> CompraIndividualExitosaData()
        {
            yield return new TestCaseData("Weapon", 100, 1, 90, 2, 1).SetName("caso_4_compra_exitosa_weapon");
            yield return new TestCaseData("Armor", 100, 1, 90, 2, 1).SetName("caso_4_compra_exitosa_armor");
            yield return new TestCaseData("Accessory", 100, 1, 90, 2, 1).SetName("caso_4_compra_exitosa_accessory");
            yield return new TestCaseData("Supply", 100, 1, 90, 2, 1).SetName("caso_4_compra_exitosa_supply");
        }

        [TestCaseSource(nameof(CompraIndividualExitosaData))]
        public void caso_4_compra_individual_exitosa_actualiza_oro_e_inventario(string category, int initialGold, int quantityToBuy, int expectedGold, int expectedStoreQuantity, int expectedPlayerQuantity)
        {
            Item item = new Item("ItemA", 10, category);
            Store store = new Store(item, 3);
            Player player = new Player(initialGold);

            bool result = store.BuyItem(player, "ItemA", category, quantityToBuy);

            Assert.That(result, Is.True);
            Assert.That(player.Gold, Is.EqualTo(expectedGold));
            Assert.That(store.GetQuantity("ItemA", category), Is.EqualTo(expectedStoreQuantity));
            Assert.That(player.Inventory.GetQuantity("ItemA", category), Is.EqualTo(expectedPlayerQuantity));
        }

        private static IEnumerable<TestCaseData> CompraFallaPorOroData()
        {
            yield return new TestCaseData(9, 2, 1).SetName("caso_4_compra_falla_oro_1");
            yield return new TestCaseData(0, 2, 1).SetName("caso_4_compra_falla_oro_2");
        }

        [TestCaseSource(nameof(CompraFallaPorOroData))]
        public void caso_4_compra_fallida_por_oro_no_modifica_estado(int playerGold, int initialStoreQuantity, int requestedQuantity)
        {
            Item sword = new Item("Espada", 10, "Weapon");
            Store store = new Store(sword, initialStoreQuantity);
            Player player = new Player(playerGold);

            bool result = store.BuyItem(player, "Espada", "Weapon", requestedQuantity);

            Assert.That(result, Is.False);
            Assert.That(player.Gold, Is.EqualTo(playerGold));
            Assert.That(store.GetQuantity("Espada", "Weapon"), Is.EqualTo(initialStoreQuantity));
            Assert.That(player.Inventory.EquipmentCount, Is.EqualTo(0));
            Assert.That(player.Inventory.SupplyCount, Is.EqualTo(0));
        }

        private static IEnumerable<TestCaseData> CompraFallaPorStockData()
        {
            yield return new TestCaseData(100, 1, 2).SetName("caso_4_compra_falla_stock_1");
            yield return new TestCaseData(100, 1, 3).SetName("caso_4_compra_falla_stock_2");
        }

        [TestCaseSource(nameof(CompraFallaPorStockData))]
        public void caso_4_compra_fallida_por_stock_no_modifica_estado(int playerGold, int initialStoreQuantity, int requestedQuantity)
        {
            Item sword = new Item("Espada", 10, "Weapon");
            Store store = new Store(sword, initialStoreQuantity);
            Player player = new Player(playerGold);

            bool result = store.BuyItem(player, "Espada", "Weapon", requestedQuantity);

            Assert.That(result, Is.False);
            Assert.That(player.Gold, Is.EqualTo(playerGold));
            Assert.That(store.GetQuantity("Espada", "Weapon"), Is.EqualTo(initialStoreQuantity));
            Assert.That(player.Inventory.EquipmentCount, Is.EqualTo(0));
            Assert.That(player.Inventory.SupplyCount, Is.EqualTo(0));
        }

        private static IEnumerable<TestCaseData> CompraEnTiendasDiferentesData()
        {
            yield return new TestCaseData(100, 70, 1, 1).SetName("caso_4_mismo_personaje_compra_en_dos_tiendas");
        }

        [TestCaseSource(nameof(CompraEnTiendasDiferentesData))]
        public void caso_4_mismo_personaje_compra_en_diferentes_tiendas(int initialGold, int expectedGold, int expectedSwordQuantity, int expectedPotionQuantity)
        {
            Store weaponStore = new Store(new Item("Espada", 20, "Weapon"), 3);
            Store supplyStore = new Store(new Item("Pocion", 10, "Supply"), 4);
            Player player = new Player(initialGold);

            bool firstResult = weaponStore.BuyItem(player, "Espada", "Weapon", 1);
            bool secondResult = supplyStore.BuyItem(player, "Pocion", "Supply", 1);

            Assert.That(firstResult, Is.True);
            Assert.That(secondResult, Is.True);
            Assert.That(player.Gold, Is.EqualTo(expectedGold));
            Assert.That(player.Inventory.GetQuantity("Espada", "Weapon"), Is.EqualTo(expectedSwordQuantity));
            Assert.That(player.Inventory.GetQuantity("Pocion", "Supply"), Is.EqualTo(expectedPotionQuantity));
        }

        private static IEnumerable<TestCaseData> CompraEnTiendasDiferentesConFallaData()
        {
            yield return new TestCaseData(25, 5, 1, 0).SetName("caso_4_una_compra_exitosa_y_otra_fallida");
        }

        [TestCaseSource(nameof(CompraEnTiendasDiferentesConFallaData))]
        public void caso_4_mismo_personaje_compra_en_dos_tiendas_y_una_falla(int initialGold, int expectedGold, int expectedSwordQuantity, int expectedPotionQuantity)
        {
            Store weaponStore = new Store(new Item("Espada", 20, "Weapon"), 3);
            Store supplyStore = new Store(new Item("Pocion", 10, "Supply"), 4);
            Player player = new Player(initialGold);

            bool firstResult = weaponStore.BuyItem(player, "Espada", "Weapon", 1);
            bool secondResult = supplyStore.BuyItem(player, "Pocion", "Supply", 1);

            Assert.That(firstResult, Is.True);
            Assert.That(secondResult, Is.False);
            Assert.That(player.Gold, Is.EqualTo(expectedGold));
            Assert.That(player.Inventory.GetQuantity("Espada", "Weapon"), Is.EqualTo(expectedSwordQuantity));
            Assert.That(player.Inventory.GetQuantity("Pocion", "Supply"), Is.EqualTo(expectedPotionQuantity));
            Assert.That(weaponStore.GetQuantity("Espada", "Weapon"), Is.EqualTo(2));
            Assert.That(supplyStore.GetQuantity("Pocion", "Supply"), Is.EqualTo(4));
        }

        private static IEnumerable<TestCaseData> CompraMultipleExitosaData()
        {
            yield return new TestCaseData(1, 2, 60, 1, 2).SetName("caso_4_compra_multiple_exitosa_1");
            yield return new TestCaseData(2, 1, 50, 2, 1).SetName("caso_4_compra_multiple_exitosa_2");
        }

        [TestCaseSource(nameof(CompraMultipleExitosaData))]
        public void caso_4_compra_multiple_exitosa_actualiza_oro_e_inventarios(int swordsToBuy, int potionsToBuy, int expectedGold, int expectedSwordQuantity, int expectedPotionQuantity)
        {
            Item sword = new Item("Espada", 20, "Weapon");
            Item potion = new Item("Pocion", 10, "Supply");

            Store store = new Store(sword, 5);
            store.AddItem(potion, 5);

            Player player = new Player(100);

            List<string> names = new List<string> { "Espada", "Pocion" };
            List<string> categories = new List<string> { "Weapon", "Supply" };
            List<int> quantities = new List<int> { swordsToBuy, potionsToBuy };

            bool result = store.BuyItems(player, names, categories, quantities);

            Assert.That(result, Is.True);
            Assert.That(player.Gold, Is.EqualTo(expectedGold));
            Assert.That(store.GetQuantity("Espada", "Weapon"), Is.EqualTo(5 - swordsToBuy));
            Assert.That(store.GetQuantity("Pocion", "Supply"), Is.EqualTo(5 - potionsToBuy));
            Assert.That(player.Inventory.GetQuantity("Espada", "Weapon"), Is.EqualTo(expectedSwordQuantity));
            Assert.That(player.Inventory.GetQuantity("Pocion", "Supply"), Is.EqualTo(expectedPotionQuantity));
        }

        private static IEnumerable<TestCaseData> CompraMultipleRepetidaData()
        {
            yield return new TestCaseData(2, 2).SetName("caso_4_compra_multiple_repetida_supera_stock_1");
            yield return new TestCaseData(1, 3).SetName("caso_4_compra_multiple_repetida_supera_stock_2");
        }

        [TestCaseSource(nameof(CompraMultipleRepetidaData))]
        public void caso_4_compra_multiple_valida_cantidad_acumulada_de_items_repetidos(int firstQuantity, int secondQuantity)
        {
            Item potion = new Item("Pocion", 5, "Supply");
            Store store = new Store(potion, 3);
            Player player = new Player(100);

            List<string> names = new List<string> { "Pocion", "Pocion" };
            List<string> categories = new List<string> { "Supply", "Supply" };
            List<int> quantities = new List<int> { firstQuantity, secondQuantity };

            bool result = store.BuyItems(player, names, categories, quantities);

            Assert.That(result, Is.False);
            Assert.That(player.Gold, Is.EqualTo(100));
            Assert.That(store.GetQuantity("Pocion", "Supply"), Is.EqualTo(3));
            Assert.That(player.Inventory.SupplyCount, Is.EqualTo(0));
        }

        private static IEnumerable<TestCaseData> ArticuloAgotadoData()
        {
            yield return new TestCaseData(1).SetName("caso_4_articulo_agotado_1");
            yield return new TestCaseData(2).SetName("caso_4_articulo_agotado_2");
        }

        [TestCaseSource(nameof(ArticuloAgotadoData))]
        public void caso_4_si_un_articulo_se_agota_ya_no_puede_comprarse(int quantity)
        {
            Item potion = new Item("Pocion", 5, "Supply");
            Store store = new Store(potion, quantity);
            Player player = new Player(100);

            bool firstResult = store.BuyItem(player, "Pocion", "Supply", quantity);
            bool secondResult = store.BuyItem(player, "Pocion", "Supply", 1);

            Assert.That(firstResult, Is.True);
            Assert.That(secondResult, Is.False);
            Assert.That(store.GetQuantity("Pocion", "Supply"), Is.EqualTo(0));
        }
    }
}