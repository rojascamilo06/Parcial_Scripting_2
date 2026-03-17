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
    }
}