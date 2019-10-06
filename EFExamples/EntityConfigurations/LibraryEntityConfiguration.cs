using EFExamples.Schema.LibraryAggregate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExamples.EntityConfigurations
{
    // класс, описывающий конфигурацию свойств обьекта Library c помощью FluentAPI
    public class LibraryEntityConfiguration : EntityTypeConfiguration<Library>
    {
        public LibraryEntityConfiguration() {
            // определяем что длина поля Name в базе данных не может быть длиннее 255 символов
            this.Property(p => p.Name).HasMaxLength(255);
            // определяем что имя библиотеки должно быть уникальным
            this.HasIndex(p => p.Name).IsUnique();
            // для того что-бы хранить даты до 1970 года
            this.Property(p => p.OpenAt).HasColumnType("datetime2");
            this.Property(p => p.OpenTill).HasColumnType("datetime2");
        }
    }
}
