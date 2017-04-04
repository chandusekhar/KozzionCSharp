package com.kozzion.library.machinelearning.classifier.naivebayes;

import java.util.ArrayList;

public class NaiveBayesEstimator 
{
	private ArrayList<Group> features;
	private double defaultChurnChance = 1;
	private double defaultNONChurnChance = 1;
	private int nonChurnCount = 0;
	private int churnCount = 0;
	
	public NaiveBayesEstimator(ArrayList<Group> features, double defaultChurnChance, double defaultNONChurnChance,  int nonChurnCount, int churnCount)
	{
		this.features=features;
		this.defaultChurnChance = defaultChurnChance;
		this.defaultNONChurnChance = defaultNONChurnChance;
		this.churnCount=churnCount;
		this.nonChurnCount=nonChurnCount;
	}
	
	public double classify(String[][] instances, int churnIndex)
	{
		int correct = 0;
		for (String[] instance:instances)
		{
			double result = estimateChurn(instance);
			int predict = result>0.5?1:0;
			if (instance[churnIndex].equals(""+predict)) correct++;
		}
		
		return correct/(instances.length+0.0);
	}
	
	public double estimateChurn(String[] instance)
	{
		double churn = defaultChurnChance;
		double nonChurn = defaultNONChurnChance;
		for (int i=0;i<instance.length;i++)
		{
			String f = instance[i];
			for (Group g:features)
			{
				if (g.match(f, i))
				{
					
					double likelihoodChurn = g.getChurnEvidence() / (churnCount+0.0);
					double likelihoodNonChurn = g.getNonChurnEvidence() / (nonChurnCount+0.0);
					
					churn*=likelihoodChurn;
					nonChurn*=likelihoodNonChurn;
					
					double evidence = (g.getChurnEvidence()+g.getNonChurnEvidence())/(churnCount+nonChurnCount+0.0); 
					
					churn/=evidence;
					nonChurn/=evidence;
					
					break;
				}
			}
		}
		//System.out.println(churn+" "+nonChurn);
		return churn;
	}
}
