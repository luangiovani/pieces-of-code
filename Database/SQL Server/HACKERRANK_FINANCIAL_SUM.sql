      SELECT ufd.first_name, 
			 ufd.last_name, 
			 ufd.vpa, 
			 CAST(SUM(BRECHD.amount) AS VARCHAR), 
		     CASE WHEN SUM(BRECHD.amount) < ufd.credit_limit AND (SUM(BRECHD.amount) + ufd.credit_limit) < 0 THEN 'YES' ELSE 'NO' END
        FROM user_financial_detail ufd
OUTER APPLY( 
			SELECT TL.amount * CASE WHEN TL.paid_by = ufd.vpa THEN -1 ELSE 1 END amount
			  FROM transaction_log TL
			 WHERE (TL.paid_by = ufd.vpa OR TL.paid_to = ufd.vpa))BRECHD
GROUP BY ufd.first_name, ufd.last_name, ufd.vpa, ufd.credit_limit