using System;
using System.Collections.Generic;

namespace APS.Data.Models;

public partial class Equipo
{
    public int EquipoId { get; set; }

    public string? Marca { get; set; }  // Ahora permite NULL

    public string? Modelo { get; set; }  // Ahora permite NULL

    public string? NombreCliente { get; set; }  // Ahora permite NULL

    public string? MotivoIngreso { get; set; }  // Ahora permite NULL

    public bool? GarantiaConLocal { get; set; }  // Ahora permite NULL

    public string? ContraseñaEquipo { get; set; }  // Sigue permitiendo NULL

    public string? Descripcion { get; set; }  // Sigue permitiendo NULL

    public DateTime? FechaIngreso { get; set; }  // Ahora permite NULL

    public int UsuarioId { get; set; }

    public virtual ICollection<Aprobacione> Aprobaciones { get; set; } = new List<Aprobacione>();

    public virtual User Usuario { get; set; } = null!;
}
