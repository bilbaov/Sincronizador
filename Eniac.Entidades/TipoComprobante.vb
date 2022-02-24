Imports System.ComponentModel
<Serializable()> 
<Description("TipoComprobante")>
Public Class TipoComprobante
   Inherits Eniac.Entidades.Entidad

   Public Enum Columnas
      IdTipoComprobante
      EsFiscal
      Descripcion
      Tipo
      CoeficienteStock
      GrabaLibro
      InformaLibroIVA
      EsFacturable
      LetrasHabilitadas
      CantidadMaximaItems
      Imprime
      CoeficienteValores
      ModificaAlFacturar
      EsFacturador
      AfectaCaja
      CargaPrecioActual
      ActualizaCtaCte
      DescripcionAbrev
      PuedeSerBorrado
      CantidadCopias
      EsRemitoTransportista
      ComprobantesAsociados
      EsComercial
      CantidadMaximaItemsObserv
      IdTipoComprobanteSecundario
      ImporteTope
      IdTipoComprobanteEpson
      UsaFacturacionRapida
      ImporteMinimo
      EsElectronico
      UtilizaImpuestos
      NumeracionAutomatica
      GeneraObservConInvocados
      IdPlanCuenta
      ImportaObservDeInvocados
      ImportaObservGrales
      IdContraComprobante
      EsRecibo
      EsAnticipo
      EsPreElectronico
      GeneraContabilidad
      UtilizaCtaSecundariaProd
      UtilizaCtaSecundariaCateg
      IncluirEnExpensas
      IdTipoComprobanteNCred
      IdTipoComprobanteNDeb
      IdProductoNCred
      IdProductoNDeb
      ConsumePedidos
      EsPreFiscal
      CodigoComprobanteSifere
      EsDespachoImportacion
      CargaDescRecActual
      IdTipoComprobanteContraMovInvocar
      AlInvocarPedidoAfectaFactura
      AlInvocarPedidoAfectaRemito
      InvocarPedidosConEstadoEspecifico
      InvocaCompras
      LargoMaximoNumero
      NivelAutorizacion
      RequiereReferenciaCtaCte
      ControlaTopeConsumidorFinal
      CargaDescRecGralActual
      EsReciboClienteVinculado
      AFIPWSIncluyeFechaVencimiento
      AFIPWSEsFEC
      AFIPWSRequiereReferenciaComercial
      AFIPWSRequiereComprobanteAsociado
      AFIPWSRequiereCBU
      AFIPWSCodigoAnulacion
      AFIPWSRequiereReferenciaTransferencia
      Orden
      MarcaInvocado
      PermiteSeleccionarAlicuotaIVA
      DescripcionImpresion
      Grupo
      SolicitaFechaDevolucion
      ActivaTildeMercDespacha
   End Enum

   Public Enum Tipos
      VENTAS
      BANCO
      COMPRAS
      CTACTE
      PAGO
      PAGOPROV
      FACT
      eFACT
      'Se quitaron para que no queden referencias explicitas, debe utilizars elas propiedades "EsRecibo" o "EsAnticipo"
      'ANTICIPO
      'ANTICIPOPROV
      RECIBO         'Reemplezar por Parametros.
      RECIBOPROV
      'MINUTA
      'MINUTAPROV
      PEDIDOSCLIE
      PRESUPCLIE
      PRODUCCION
      eNCRED
      eNDEB
      NCRED
      NDEB
      '------------------------------------------------------------
   End Enum

   Public Enum ValoresFijosIdTipoComprobante As Integer
      <Description("(Selección Multiple)")> SeleccionMultiple = -1
      <Description("(Todos)")> Todos = -2
   End Enum

   Public Sub New()
      NivelAutorizacion = 1

      _IdTipoComprobante = ""
      _Descripcion = ""
      _LetrasHabilitadas = ""
      _CantidadMaximaItems = 0
      _ActualizaCtaCte = False
      _cantidadCopias = 1
      _CantidadMaximaItemsObserv = 0
      _ImporteTope = 0
      _ImporteMinimo = 0
   End Sub

#Region "Propiedades"

   Public Property IdTipoComprobante As String
   Public Property EsFiscal As Boolean
   Public Property Descripcion As String
   Public Property Tipo As String
   Public Property CoeficienteStock As Integer
   Public Property GrabaLibro As Boolean
   Public Property InformaLibroIVA As Boolean
   Public Property EsFacturable As Boolean
   Public Property LetrasHabilitadas As String
   Public Property CantidadMaximaItems As Integer
   Public Property Imprime As Boolean
   Public Property CoeficienteValores As Integer
   Public Property ModificaAlFacturar As String
   Public Property EsFacturador As Boolean
   Public Property AfectaCaja As Boolean
   Public Property CargaPrecioActual As Boolean
   Public Property ActualizaCtaCte As Boolean
   Public Property DescripcionAbrev As String
   Public Property PuedeSerBorrado As Boolean
   Public Property CantidadCopias As Integer
   Public Property EsRemitoTransportista As Boolean
   Public Property ComprobantesAsociados As String
   Public Property EsComercial As Boolean
   Public Property CantidadMaximaItemsObserv As Integer
   Public Property IdTipoComprobanteSecundario As String
   Public Property ImporteTope As Decimal
   Public Property IdTipoComprobanteEpson As String
   Public Property UsaFacturacionRapida As Boolean
   Public Property UsaFacturacion As Boolean
   Public Property ImporteMinimo As Decimal
   Public Property EsElectronico As Boolean
   Public Property UtilizaImpuestos As Boolean
   Public Property NumeracionAutomatica As Boolean
   Public Property GeneraObservConInvocados As Boolean
   Public Property ImportaObservDeInvocados As Boolean
   Public Property ImportaObservGrales As Boolean
   Public Property IdPlanCuenta As Integer
   Public Property EsRecibo As Boolean
   Public Property EsAnticipo As Boolean
   Public Property EsPreElectronico As Boolean
   Public Property GeneraContabilidad As Boolean
   Public Property Grupo As String

   Public Property UtilizaCtaSecundariaProd As Boolean
   Public Property UtilizaCtaSecundariaCateg As Boolean

   Public Property IncluirEnExpensas As Boolean

   Public Property IdTipoComprobanteNCred As String
   Public Property IdTipoComprobanteNDeb As String

   Public Property IdProductoNCred As String
   Public Property IdProductoNDeb As String

   Public Property ConsumePedidos As Boolean

   Public Property EsPreFiscal As Boolean
   Public Property CodigoComprobanteSifere As String
   Public Property EsDespachoImportacion As Boolean
   Public Property CargaDescRecActual As Boolean
   Public Property IdTipoComprobanteContraMovInvocar As String
   Public Property AlInvocarPedidoAfectaFactura As Boolean
   Public Property AlInvocarPedidoAfectaRemito As Boolean
   Public Property InvocarPedidosConEstadoEspecifico As Boolean
   Public Property InvocaCompras As Boolean
   Public Property LargoMaximoNumero As Short
   Public Property RequiereReferenciaCtaCte As Boolean
   Public Property NivelAutorizacion As Short
   Public Property ControlaTopeConsumidorFinal As Boolean
   Public Property CargaDescRecGralActual As Boolean
   Public Property EsReciboClienteVinculado As Boolean
   Public Property AFIPWSIncluyeFechaVencimiento As Boolean?
   Public Property AFIPWSEsFEC As Boolean
   Public Property AFIPWSRequiereReferenciaComercial As Boolean
   Public Property AFIPWSRequiereComprobanteAsociado As Boolean
   Public Property AFIPWSRequiereCBU As Boolean
   Public Property AFIPWSCodigoAnulacion As Boolean
   Public Property AFIPWSRequiereReferenciaTransferencia As Boolean
   Public Property Orden As Integer
   Public Property MarcaInvocado As Boolean
   Public Property PermiteSeleccionarAlicuotaIVA As Boolean
   Public Property DescripcionImpresion As String
   Public Property SolicitaFechaDevolucion As Boolean
   Public Property ActivaTildeMercDespacha As Boolean

   Private _productos As List(Of TipoComprobanteProducto)
   Public Property Productos As List(Of TipoComprobanteProducto)
      Get
         If _productos Is Nothing Then _productos = New List(Of TipoComprobanteProducto)()
         Return _productos
      End Get
      Set(value As List(Of TipoComprobanteProducto))
         _productos = value
      End Set
   End Property

#End Region

   Public Overridable Function GetCopia() As Entidades.TipoComprobante
      Dim copia As Entidades.TipoComprobante = New Entidades.TipoComprobante()
      With copia
         .ActualizaCtaCte = Me._ActualizaCtaCte
         .AfectaCaja = Me._AfectaCaja
         .CantidadCopias = Me._CantidadCopias
         .CantidadMaximaItems = Me._CantidadMaximaItems
         .CantidadMaximaItemsObserv = Me._CantidadMaximaItemsObserv
         .CargaPrecioActual = Me._CargaPrecioActual
         .CoeficienteStock = Me._CoeficienteStock
         .CoeficienteValores = Me._CoeficienteValores
         .ComprobantesAsociados = Me._ComprobantesAsociados
         .Descripcion = Me._Descripcion
         .DescripcionAbrev = Me._DescripcionAbrev
         .EsComercial = Me._EsComercial
         .EsElectronico = Me._EsElectronico
         .EsFacturable = Me._EsFacturable
         .EsFacturador = Me._EsFacturador
         .EsFiscal = Me._EsFiscal
         .EsRemitoTransportista = Me._EsRemitoTransportista
         .GeneraObservConInvocados = Me._GeneraObservConInvocados
         .IdPlanCuenta = Me._IdPlanCuenta
         .ImportaObservDeInvocados = Me._ImportaObservDeInvocados
         .ImportaObservGrales = Me._ImportaObservGrales
         .GrabaLibro = Me._GrabaLibro
         .InformaLibroIVA = Me._InformaLibroIVA
         .IdSucursal = Me.IdSucursal
         .IdTipoComprobante = Me._IdTipoComprobante
         .IdTipoComprobanteEpson = Me._IdTipoComprobanteEpson
         .IdTipoComprobanteSecundario = Me._IdTipoComprobanteSecundario
         .ImporteMinimo = Me._ImporteMinimo
         .ImporteTope = Me._ImporteTope
         .Imprime = Me._Imprime
         .LetrasHabilitadas = Me._LetrasHabilitadas
         .ModificaAlFacturar = Me._ModificaAlFacturar
         .NumeracionAutomatica = Me._NumeracionAutomatica
         .Password = Me.Password
         .PuedeSerBorrado = Me._PuedeSerBorrado
         .Tipo = Me._Tipo
         .UsaFacturacionRapida = Me._UsaFacturacionRapida
         .Usuario = Me.Usuario
         .UtilizaImpuestos = Me._UtilizaImpuestos
         .EsRecibo = Me._EsRecibo
         .EsAnticipo = Me._EsAnticipo
         .EsPreElectronico = Me._EsPreElectronico
         .GeneraContabilidad = Me.GeneraContabilidad
         .UtilizaCtaSecundariaProd = Me.UtilizaCtaSecundariaProd
         .UtilizaCtaSecundariaCateg = Me.UtilizaCtaSecundariaCateg
         .IncluirEnExpensas = Me.IncluirEnExpensas
         .IdTipoComprobanteNCred = Me.IdTipoComprobanteNCred
         .IdTipoComprobanteNDeb = Me.IdTipoComprobanteNDeb
         .IdProductoNCred = Me.IdProductoNCred
         .IdProductoNDeb = Me.IdProductoNDeb
         .ConsumePedidos = Me.ConsumePedidos
         .CodigoComprobanteSifere = Me.CodigoComprobanteSifere
         .EsPreFiscal = Me.EsPreFiscal
         .CargaDescRecActual = Me.CargaDescRecActual
         .IdTipoComprobanteContraMovInvocar = Me.IdTipoComprobanteContraMovInvocar
         .AlInvocarPedidoAfectaFactura = Me.AlInvocarPedidoAfectaFactura
         .AlInvocarPedidoAfectaRemito = Me.AlInvocarPedidoAfectaRemito
         .InvocarPedidosConEstadoEspecifico = Me.InvocarPedidosConEstadoEspecifico
         .InvocaCompras = Me.InvocaCompras
         .LargoMaximoNumero = Me.LargoMaximoNumero
         .RequiereReferenciaCtaCte = Me.RequiereReferenciaCtaCte
         .NivelAutorizacion = Me.NivelAutorizacion
         .CargaDescRecGralActual = Me.CargaDescRecGralActual
         .EsReciboClienteVinculado = Me.EsReciboClienteVinculado

         .AFIPWSIncluyeFechaVencimiento = Me.AFIPWSIncluyeFechaVencimiento
         .AFIPWSEsFEC = Me.AFIPWSEsFEC
         .AFIPWSRequiereReferenciaComercial = Me.AFIPWSRequiereReferenciaComercial
         .AFIPWSRequiereComprobanteAsociado = Me.AFIPWSRequiereComprobanteAsociado
         .AFIPWSRequiereCBU = Me.AFIPWSRequiereCBU
         .AFIPWSCodigoAnulacion = Me.AFIPWSCodigoAnulacion
         .AFIPWSRequiereReferenciaTransferencia = Me.AFIPWSRequiereReferenciaTransferencia
         .Orden = Me.Orden
         .MarcaInvocado = Me.MarcaInvocado
         .PermiteSeleccionarAlicuotaIVA = Me.PermiteSeleccionarAlicuotaIVA
         .DescripcionImpresion = DescripcionImpresion
         '-- REQ-30773 --
         .ActivaTildeMercDespacha = Me.ActivaTildeMercDespacha
      End With
      Return copia
   End Function
End Class