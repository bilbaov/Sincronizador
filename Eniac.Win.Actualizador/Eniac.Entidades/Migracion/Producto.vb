﻿Namespace Entidades
   <Obsolete("", True)>
   Public Class Producto
      Inherits Entidad

      Public Const NombreTabla As String = "Productos"

      Public Enum Columnas
         IdProducto
         NombreProducto
         Tamano
         IdUnidadDeMedida
         IdUnidadDeMedida2
         Conversion
         IdMarca
         IdRubro
         MesesGarantia
         Activo
         AfectaStock
         IdModelo
         IdSubRubro
         Foto
         PermiteModificarDescripcion
         Lote
         NroSerie
         IdTipoImpuesto
         Alicuota
         CodigoDeBarras
         CodigoDeBarrasConPrecio
         EsServicio
         Observacion

         PublicarEnWeb
         PublicarListaDePreciosClientes
         PublicarEnGestion
         PublicarEnEmpresarial
         PublicarEnBalanza
         PublicarEnSincronizacionSucursal

         EsDeCompras
         EsDeVentas
         DescontarStockComponentes
         IdMoneda
         EsCompuesto
         EsAlquilable
         EquipoMarca
         EquipoModelo
         NumeroSerie
         CaractSII
         Anio
         MultipleIva
         PagaIngresosBrutos
         Embalaje
         Kilos
         IdFormula
         IdProductoMercosur
         IdProductoSecretaria
         FacturacionCantidadNegativa
         SolicitaEnvase
         AlertaDeCaja
         NombreCorto
         EsRetornable
         Orden
         IdProveedor
         CodigoLargoProducto
         ModalidadCodigoDeBarras
         EsObservacion
         UnidHasta1
         UnidHasta2
         UnidHasta3
         UnidHasta4
         UnidHasta5
         UHPorc1
         UHPorc2
         UHPorc3
         UHPorc4
         UHPorc5
         PrecioPorEmbalaje
         IdCuentaCompras
         IdCuentaVentas
         ImporteImpuestoInterno
         EsOferta
         IdCuentaComprasSecundaria
         IncluirExpensas
         IdCentroCosto
         ObservacionCompras
         SolicitaPrecioVentaPorTamano
         Espmm
         EspPulgadas
         CodigoSAE
         IdProduccionProceso
         IdProduccionForma
         CalculaPreciosSegunFormula

         PorcImpuestoInterno
         OrigenPorcImpInt
         Alicuota2
         IdSubSubRubro

         EsCambiable
         EsBonificable

         FechaActualizacionWeb

         Alto
         Ancho
         Largo
         DescRecProducto
         ActualizaPreciosSucursalesAsociadas

         CalidadStatusLiberado
         CalidadFechaLiberado
         CalidadUserLiberado
         CalidadStatusEntregado
         CalidadFechaEntregado
         CalidadUserEntregado
         CalidadFechaIngreso
         CalidadFechaEgreso
         CalidadNroCertificado
         CalidadFecCertificado
         CalidadUsrCertificado
         CalidadObservaciones
         CalidadFechaPreEnt
         CalidadFechaEntProg
         EsComercial
         CalidadNumeroChasis
         CalidadNroCarroceria

         IdProductoNumerico

         EnviarSinCargo

         CodigoProductoTiendaNube
         CodigoProductoMercadoLibre
         idCategoriaMercadoLibre

         '-- REQ-31619.- --
         NombreProductoWeb

         IdVarianteProducto
         IdProductoTipoServicio
         NombreProductoTipoServicio
         CalidadNroDeMotor
         CalidadFechaEntReProg
         IdProductoModelo
         NombreProductoModelo

         CalidadNroCertEntregado
         CalidadFecCertEntregado
         CalidadUsrCertEntregado

         CalidadStatusLiberadoPDI
         CalidadFechaLiberadoPDI
         CalidadUserLiberadoPDI

      End Enum

      Public Enum Modalidades
         NORMAL
         PESO
         PRECIO
      End Enum

      Public Enum TiposOperaciones As Integer
         NORMAL = 0
         BONIFICACION = 1
         CAMBIO = 2
      End Enum

      Public Enum ExportarEnviando
         <Description("Pendientes Exportar")> PENDIENTES
         <Description("Según Fecha de Enviado")> FECHAENVIO
         <Description("Reenviar Todos")> TODOS
      End Enum

#Region "Campos"

      Private _idMarca As Integer = 0
      Private _idModelo As Integer = 0
      Private _idRubro As Integer = 0
      Private _idSubRubro As Integer = 0
      Private _mesesGarantia As Integer = 0
      Private _idTipoImpuesto As String = ""
      Private _alicuota As Decimal = 0
      Private _afectaStock As Boolean = False
      Private _activo As Boolean = False
      Private _ubicacion As String = String.Empty

#End Region

      Public Sub New()
         'Identificaciones = New List(Of ProductoIdentificacion)()
         'ActualizaPreciosSucursalesAsociadas = True
      End Sub

#Region "Propiedades"

      Private _idProducto As String = ""
      Public Property IdProducto() As String
         Get
            Return Me._idProducto
         End Get
         Set(ByVal value As String)
            Me._idProducto = value.Trim()
         End Set
      End Property

      Private _nombreProducto As String = ""
      Public Property NombreProducto() As String
         Get
            Return Me._nombreProducto
         End Get
         Set(ByVal value As String)
            Me._nombreProducto = value.Trim()
         End Set
      End Property

      Private _nombreProductoWeb As String = ""
      Public Property NombreProductoWeb() As String
         Get
            Return Me._nombreProductoWeb
         End Get
         Set(ByVal value As String)
            Me._nombreProductoWeb = value.Trim()
         End Set
      End Property

      Private _tamano As Decimal
      Public Property Tamano() As Decimal
         Get
            Return Me._tamano
         End Get
         Set(ByVal value As Decimal)
            Me._tamano = value
         End Set
      End Property

      Public Property IdMarca() As Integer
         Get
            Return Me._idMarca
         End Get
         Set(ByVal value As Integer)
            Me._idMarca = value
         End Set
      End Property

      Public Property IdModelo() As Integer
         Get
            Return Me._idModelo
         End Get
         Set(ByVal value As Integer)
            Me._idModelo = value
         End Set
      End Property

      Public Property IdRubro() As Integer
         Get
            Return Me._idRubro
         End Get
         Set(ByVal value As Integer)
            Me._idRubro = value
         End Set
      End Property

      Public Property IdSubRubro() As Integer
         Get
            Return Me._idSubRubro
         End Get
         Set(ByVal value As Integer)
            Me._idSubRubro = value
         End Set
      End Property

      Public Property MesesGarantia() As Integer
         Get
            Return _mesesGarantia
         End Get
         Set(ByVal value As Integer)
            _mesesGarantia = value
         End Set
      End Property

      Public Property IdTipoImpuesto() As String
         Get
            Return _idTipoImpuesto
         End Get
         Set(ByVal value As String)
            _idTipoImpuesto = value
         End Set
      End Property

      Public Property Alicuota() As Decimal
         Get
            Return Me._alicuota
         End Get
         Set(ByVal value As Decimal)
            Me._alicuota = value
         End Set
      End Property

      Public Property AfectaStock() As Boolean
         Get
            Return _afectaStock
         End Get
         Set(ByVal value As Boolean)
            _afectaStock = value
         End Set
      End Property

      Public Property Activo() As Boolean
         Get
            Return _activo
         End Get
         Set(ByVal value As Boolean)
            _activo = value
         End Set
      End Property

      Private _foto As System.Drawing.Image
      Public Property Foto() As System.Drawing.Image
         Get
            Return _foto
         End Get
         Set(ByVal value As System.Drawing.Image)
            _foto = value
         End Set
      End Property

      Private _permiteModificarDescripcion As Boolean
      Public Property PermiteModificarDescripcion() As Boolean
         Get
            Return Me._permiteModificarDescripcion
         End Get
         Set(ByVal value As Boolean)
            Me._permiteModificarDescripcion = value
         End Set
      End Property

      Private _lote As Boolean = False
      Public Property Lote() As Boolean
         Get
            Return _lote
         End Get
         Set(ByVal value As Boolean)
            _lote = value
         End Set
      End Property

      Private _nroSerie As Boolean = False
      Public Property NroSerie() As Boolean
         Get
            Return _nroSerie
         End Get
         Set(ByVal value As Boolean)
            _nroSerie = value
         End Set
      End Property

      Private _codigoDeBarras As String = String.Empty
      Public Property CodigoDeBarras() As String
         Get
            Return Me._codigoDeBarras
         End Get
         Set(ByVal value As String)
            Me._codigoDeBarras = value
         End Set
      End Property

      Private _codigoDeBarrasConPrecio As Boolean
      Public Property CodigoDeBarrasConPrecio() As Boolean
         Get
            Return Me._codigoDeBarrasConPrecio
         End Get
         Set(ByVal value As Boolean)
            Me._codigoDeBarrasConPrecio = value
         End Set
      End Property

      Private _esServicio As Boolean
      Public Property EsServicio() As Boolean
         Get
            Return _esServicio
         End Get
         Set(ByVal value As Boolean)
            _esServicio = value
         End Set
      End Property

      Public Property PublicarEnWeb As Boolean
      Public Property PublicarListaDePreciosClientes As Boolean
      Public Property PublicarEnGestion As Boolean
      Public Property PublicarEnEmpresarial As Boolean
      Public Property PublicarEnBalanza As Boolean
      Public Property PublicarEnSincronizacionSucursal As Boolean


      Private _observacion As String
      Public Property Observacion() As String
         Get
            Return Me._observacion
         End Get
         Set(ByVal value As String)
            Me._observacion = value
         End Set
      End Property

      Private _esDeCompras As Boolean
      Public Property EsDeCompras() As Boolean
         Get
            Return _esDeCompras
         End Get
         Set(ByVal value As Boolean)
            _esDeCompras = value
         End Set
      End Property

      Private _esDeVentas As Boolean
      Public Property EsDeVentas() As Boolean
         Get
            Return _esDeVentas
         End Get
         Set(ByVal value As Boolean)
            _esDeVentas = value
         End Set
      End Property

      Private _esCompuesto As Boolean
      Public Property EsCompuesto() As Boolean
         Get
            Return Me._esCompuesto
         End Get
         Set(ByVal value As Boolean)
            Me._esCompuesto = value
         End Set
      End Property

      Private _descStockComp As Boolean
      Public Property DescontarStockComponentes() As Boolean
         Get
            Return _descStockComp
         End Get
         Set(ByVal value As Boolean)
            _descStockComp = value
         End Set
      End Property

      Private _esAlquilable As Boolean
      Public Property EsAlquilable() As Boolean
         Get
            Return Me._esAlquilable
         End Get
         Set(ByVal value As Boolean)
            Me._esAlquilable = value
         End Set
      End Property

      Private _equipoMarca As String
      Public Property EquipoMarca() As String
         Get
            Return Me._equipoMarca
         End Get
         Set(ByVal value As String)
            Me._equipoMarca = value
         End Set
      End Property

      Private _equipoModelo As String
      Public Property EquipoModelo() As String
         Get
            Return Me._equipoModelo
         End Get
         Set(ByVal value As String)
            Me._equipoModelo = value
         End Set
      End Property

      Private _numeroSerie As String
      Public Property NumeroSerie() As String
         Get
            Return Me._numeroSerie
         End Get
         Set(ByVal value As String)
            Me._numeroSerie = value
         End Set
      End Property

      Private _caractSII As String
      Public Property CaractSII() As String
         Get
            Return Me._caractSII
         End Get
         Set(ByVal value As String)
            Me._caractSII = value
         End Set
      End Property

      Private _anio As Integer
      Public Property Anio() As Integer
         Get
            Return Me._anio
         End Get
         Set(ByVal value As Integer)
            Me._anio = value
         End Set
      End Property

      Private _alicuota2 As Decimal?
      Public Property Alicuota2() As Decimal?
         Get
            Return Me._alicuota2
         End Get
         Set(ByVal value As Decimal?)
            Me._alicuota2 = value
         End Set
      End Property

      Private _pagaIngresosBrutos As Boolean
      Public Property PagaIngresosBrutos() As Boolean
         Get
            Return Me._pagaIngresosBrutos
         End Get
         Set(ByVal value As Boolean)
            Me._pagaIngresosBrutos = value
         End Set
      End Property

      Private _embalaje As Integer = 0
      Public Property Embalaje() As Integer
         Get
            Return _embalaje
         End Get
         Set(ByVal value As Integer)
            _embalaje = value
         End Set
      End Property

      Private _kilos As Decimal = 0
      Public Property Kilos() As Decimal
         Get
            Return _kilos
         End Get
         Set(ByVal value As Decimal)
            _kilos = value
         End Set
      End Property

      Private _idFormula As Integer
      Public Property IdFormula() As Integer
         Get
            Return Me._idFormula
         End Get
         Set(ByVal value As Integer)
            Me._idFormula = value
         End Set
      End Property

      Private _idProductoMercosur As String
      Public Property IdProductoMercosur() As String
         Get
            Return _idProductoMercosur
         End Get
         Set(ByVal value As String)
            _idProductoMercosur = value
         End Set
      End Property

      Private _idProductoSecretaria As String
      Public Property IdProductoSecretaria() As String
         Get
            Return _idProductoSecretaria
         End Get
         Set(ByVal value As String)
            _idProductoSecretaria = value
         End Set
      End Property

      Private _facturacionCantidadNegativa As Boolean
      Public Property FacturacionCantidadNegativa() As Boolean
         Get
            Return Me._facturacionCantidadNegativa
         End Get
         Set(ByVal value As Boolean)
            Me._facturacionCantidadNegativa = value
         End Set
      End Property

      Private _solicitaEnvase As Boolean
      Public Property SolicitaEnvase() As Boolean
         Get
            Return _solicitaEnvase
         End Get
         Set(ByVal value As Boolean)
            _solicitaEnvase = value
         End Set
      End Property

      Private _alertaDeCaja As Boolean
      Public Property AlertaDeCaja() As Boolean
         Get
            Return _alertaDeCaja
         End Get
         Set(ByVal value As Boolean)
            _alertaDeCaja = value
         End Set
      End Property

      Private _nombreCorto As String = ""
      Public Property nombreCorto() As String
         Get
            Return Me._nombreCorto
         End Get
         Set(ByVal value As String)
            Me._nombreCorto = value.Trim()
         End Set
      End Property

      Private _esRetornable As Boolean
      Public Property EsRetornable() As Boolean
         Get
            Return _esRetornable
         End Get
         Set(ByVal value As Boolean)
            _esRetornable = value
         End Set
      End Property

      Private _Orden As Integer

      Public Property Orden() As Integer
         Get
            Return Me._Orden
         End Get
         Set(ByVal value As Integer)
            Me._Orden = value
         End Set
      End Property

      Private _codigoLargoProducto As String
      Public Property CodigoLargoProducto() As String
         Get
            Return _codigoLargoProducto
         End Get
         Set(ByVal value As String)
            _codigoLargoProducto = value
         End Set
      End Property

      Private _precios As DataTable
      Public Property Precios() As DataTable
         Get
            Return _precios
         End Get
         Set(ByVal value As DataTable)
            _precios = value
         End Set
      End Property

      Private _precioCosto As Decimal
      Public Property PrecioCosto() As Decimal
         Get
            Return _precioCosto
         End Get
         Set(ByVal value As Decimal)
            _precioCosto = value
         End Set
      End Property

      Private _modalidadCodigoDeBarras As String
      Public Property ModalidadCodigoDeBarras() As String
         Get
            Return _modalidadCodigoDeBarras
         End Get
         Set(ByVal value As String)
            _modalidadCodigoDeBarras = value
         End Set
      End Property

      Private _esObservacion As Boolean
      Public Property EsObservacion() As Boolean
         Get
            Return _esObservacion
         End Get
         Set(ByVal value As Boolean)
            _esObservacion = value
         End Set
      End Property

      Private _UnidHasta1 As Decimal
      Public Property UnidHasta1() As Decimal
         Get
            Return Me._UnidHasta1
         End Get
         Set(ByVal value As Decimal)
            Me._UnidHasta1 = value
         End Set
      End Property

      Private _UnidHasta2 As Decimal
      Public Property UnidHasta2() As Decimal
         Get
            Return Me._UnidHasta2
         End Get
         Set(ByVal value As Decimal)
            Me._UnidHasta2 = value
         End Set
      End Property


      Private _UnidHasta3 As Decimal
      Public Property UnidHasta3() As Decimal
         Get
            Return Me._UnidHasta3
         End Get
         Set(ByVal value As Decimal)
            Me._UnidHasta3 = value
         End Set
      End Property

      Public Property UnidHasta4() As Decimal
      Public Property UnidHasta5() As Decimal

      Private _UHPorc1 As Decimal
      Public Property UHPorc1() As Decimal
         Get
            Return Me._UHPorc1
         End Get
         Set(ByVal value As Decimal)
            Me._UHPorc1 = value
         End Set
      End Property

      Private _UHPorc2 As Decimal
      Public Property UHPorc2() As Decimal
         Get
            Return Me._UHPorc2
         End Get
         Set(ByVal value As Decimal)
            Me._UHPorc2 = value
         End Set
      End Property

      Private _UHPorc3 As Decimal
      Public Property UHPorc3() As Decimal
         Get
            Return Me._UHPorc3
         End Get
         Set(ByVal value As Decimal)
            Me._UHPorc3 = value
         End Set
      End Property

      Public Property UHPorc4() As Decimal
      Public Property UHPorc5() As Decimal

      Private _precioPorEmbalaje As Boolean = False
      Public Property PrecioPorEmbalaje() As Boolean
         Get
            Return _precioPorEmbalaje
         End Get
         Set(ByVal value As Boolean)
            _precioPorEmbalaje = value
         End Set
      End Property

      Public Property Ubicacion() As String
         Get
            Return Me._ubicacion
         End Get
         Set(ByVal value As String)
            Me._ubicacion = value
         End Set
      End Property

   End Class
#End Region

End Namespace
