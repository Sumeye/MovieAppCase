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
    public class MoviesVoteConfiguration : IEntityTypeConfiguration<MovieVotes>
    {
        public void Configure(EntityTypeBuilder<MovieVotes> builder)
        {
            builder.HasKey(x => x.MovieVoteId);
            builder.Property(x => x.Note);
            builder.Property(x => x.Vote);
            builder.ToTable(nameof(MovieVotes));
        }
    }
}
