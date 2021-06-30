namespace Domain.Helpers
{
    public static class Templates
    {
        public static string HTML_EMAIL_NOVA_RECOMENDACAO => @"
<table cellpadding='0' cellspacing='0' style='width:100%; font-size: 15pt;'>
	<tbody>
		<tr>
			<td style='vertical-align:top' colspan='4'>
				<img alt='Image' src='cid:imgheadlogo' />
			</td>
		</tr>
		<tr>
			<td colspan='4'>
				<p>Ol&aacute; <strong>#DESTINATARIO#</strong>,</p>
				<p>Foi adicionado um novo reconhecimento.: <strong>#CODIGO#</strong> em <strong>#DATA#</strong></p>
			</td>
		</tr>			
		<tr>
			<td colspan='4'>
				<p>#TEXTOAPROVAADICIONAL#</p>
			</td>
		</tr>
		<tr>
			<td style='width:150px'>
				<p>Gestor Solicitante.:</p>
			</td>
			<td colspan='3'>
				<p><strong>#CSSOLIC# - #NOMESOLIC#</strong></p>
			</td>
		</tr>
		<tr>
			<td style='width:150px'>
				<p>Colaborador.:</p>
			</td>
			<td colspan='3'>
				<p><strong>#CS# - #NOME#</strong></p>
			</td>
		</tr>
		<tr>
			<td style='width:150px'>
				<p>Gestor Colaborador.:</p>
			</td>
			<td colspan='3'>
				<p><strong>#CSGESTOR# - #NOMEGESTOR#</strong></p>
			</td>
		</tr>
		<tr>
			<td style='width:150px'>
				<p>Pontos.:</p>
			</td>
			<td colspan='3'>
				<p><strong>#PONTOS#</strong></p>
			</td>
		</tr>
		<tr>
			<td style='width:150px'>
				<p>Motivo.:</p>
			</td>
			<td colspan='3'>
				<p><strong>#MOTIVO#</strong></p>
			</td>
		</tr>
		<tr>
			<td style='width:150px'>
				<p>Justificativa.:</p>
			</td>
			<td colspan='3'>
				<p><strong>#JUSTIFICATIVA#</strong></p>
			</td>
		</tr>
	</tbody>
</table>";

        public static string HTML_RECOMENDACAO_APROVADA => @"
<table cellpadding='0' cellspacing='0' style='width:100% font-size: 15pt;'>
	<tbody>
		<tr>
			<td style='vertical-align:top' colspan='2'>
				<img alt='Image' src='cid:imgheadlogo' />
			</td>
		</tr>
		<tr>
			<td colspan='2'>
				<p>Ol&aacute; <strong>#DESTINATARIO#</strong>,</p>
				<p>Parabéns!!! Você recebeu uma nova recomendação e seus pontos já estão disponíveis para trocar por produtos!</p>
			</td>
		</tr>			
		<tr>
			<td style='width:150px'>
				<p>Pontos.:</p>
			</td>
			<td>
				<p><strong>#PONTOS#</strong></p>
			</td>
		</tr>
		<tr>
			<td style='width:150px'>
				<p>Motivo.:</p>
			</td>
			<td>
				<p><strong>#MOTIVO#</strong></p>
			</td>
		</tr>
		<tr>
			<td style='width:150px'>
				<p>Justificativa.:</p>
			</td>
			<td>
				<p><strong>#JUSTIFICATIVA#</strong></p>
			</td>
		</tr>
	</tbody>
</table>";

        public static string HTML_VERBA_GESTOR => @"
<table cellpadding='0' cellspacing='0' style='width:100% font-size: 15pt;'>
	<tbody>
		<tr>
			<td style='vertical-align:top' colspan='2'>
				<img alt='Image' src='cid:imgheadlogo' />
			</td>
		</tr>
		<tr>
			<td colspan='2'>
				<p>Ol&aacute; <strong>#DESTINATARIO#</strong>,</p>
				<p>Você acaba de receber uma atualização em sua Verba (Saldo de Pontos) para Reconhecimento de Colaboradores, verifique agora mesmo!</p>
			</td>
		</tr>			
		<tr>
			<td style='width:150px'>
				<p>Pontos.:</p>
			</td>
			<td>
				<p><strong>#PONTOS#</strong></p>
			</td>
		</tr>
	</tbody>
</table>";

        public static string HTML_GENERICO => @"
<table cellpadding='0' cellspacing='0' style='width:100% font-size: 15pt;'>
	<tbody>
		<tr>
			<td style='vertical-align:top' colspan='2'>
				<img alt='Image' src='cid:imgheadlogo' />
			</td>
		</tr>
		<tr>
			<td colspan='2'>
				<p>Ol&aacute; <strong>#DESTINATARIO#</strong>,</p>
				<p>#MENSAGEM#</p>
			</td>
		</tr>			
	</tbody>
</table>";
    }
}
