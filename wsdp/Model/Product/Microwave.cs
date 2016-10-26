using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Product {
	public class Microwave {
		public int Code { get; set; }
		public string Name { get; set; }
		public string ShopName { get; set; }
		public string PhotoUrl { get; set; }
		public decimal Price { get; set; }
		public string InstallationMethod { get; set; }
		public string Type { get; set; }
		public string ControlType { get; set; }
		public string Volume { get; set; }
		public string SvtPower { get; set; }
		public string DoorOpenType { get; set; }
		public string GrillType { get; set; }
		public string GrillPower { get; set; }
		public string DiameterOfTurnable { get; set; }
		public string InternalCoatingChamber { get; set; }
		public string CountOfPowerLevels { get; set; }
		public string Functions { get; set; }
		public string Fuatures { get; set; }
		public string Equipment { get; set; }
		public string NicheDimensions { get; set; }
		public string Dimensions { get; set; }
		public string DimensionsInPackege { get; set; }
		public string Weight { get; set; }
		public string WeightInPackege { get; set; }
		public string Color { get; set; }

		/*public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.Append($"Магазин: {ShopName}");
			builder.AppendLine($"Код товара: {Code}");
			builder.AppendLine($"Название: {Name}");
			builder.AppendLine($"Цена: {Price}");
			builder.AppendLine($"Метод установки: {InstallationMethod}");
			builder.AppendLine($"Тип: {Type}");
			builder.AppendLine($"Тип управления: {ControlType}");
			builder.AppendLine($"Объем: {Volume}");
			builder.AppendLine($"Мощность СВЧ: {SvtPower}");
			builder.AppendLine($"Тип открытия дверцы: {DoorOpenType}");
			builder.AppendLine($"Тип гриля: {GrillType}");
			builder.AppendLine($"Мощность гриля: {GrillPower}");
			builder.AppendLine($"Диаметр поворотного стола: {DiameterOfTurnable}");
			builder.AppendLine($"Внутреннее покрытие камеры: {InternalCoatingChamber}");
			builder.AppendLine($"Количество уровней мощности: {CountOfPowerLevels}");
			builder.AppendLine($"Функции и специальные программы: {Functions}");
			builder.AppendLine($"Особенности: {Fuatures}");
			builder.AppendLine($"Комплектация: {Equipment}");
			builder.AppendLine($"Размеры ниши для встраивания(ВхШхГ): {NicheDimensions}");
			builder.AppendLine($"Габариты (ВхШхГ): {Dimensions}");
			builder.AppendLine($"Габариты в упаковке(ВхШхГ): {DimensionsInPackege}");
			builder.AppendLine($"Вес: {Weight}");
			builder.AppendLine($"Вес в упаковке: {WeightInPackege}");
			builder.AppendLine($"Цвет: {Color}");
			return builder.ToString();
		}*/
	}
}
