using Kesh.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Persistance.EntityConfiguration;
public class AccessTokenConfiguration : IEntityTypeConfiguration<AccessToken>
{
    public void Configure(EntityTypeBuilder<AccessToken> builder)
    {
        builder.Property(accessToken => accessToken.Token).IsRequired().HasMaxLength(1024);

        builder.HasOne<User>().WithMany().HasForeignKey(accessToken => accessToken.UserId);
    }
}
