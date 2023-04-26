using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieAppCase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Repository.Configuration
{
    public class MoviesConfiguration : IEntityTypeConfiguration<Movies>
    {
        public void Configure(EntityTypeBuilder<Movies> builder)
        {
            builder.HasKey(x => x.MovieId);
            builder.Property(x => x.Overview);
            builder.Property(x => x.PosterPath);
            builder.Property(x => x.ReleaseDate);
            builder.Property(x => x.SourceId);
            builder.Property(x => x.Title);
            builder.ToTable(nameof(Movies));
        }
    }
}
