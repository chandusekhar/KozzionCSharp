package com.kozzion.library.machinelearning.classifier.naivebayes;


import java.util.ArrayList;
import java.util.List;

import com.kozzion.library.math.function.domain.implementation.DoubleSamplingDomain;
import com.kozzion.library.math.statistics.FMeasure;
import com.kozzion.library.math.statistics.distribution.interfaces.IDistributionDoubleNumber;
import com.kozzion.library.math.utility.ParzenDistributionGenerator;

public class NaiveBayesClassifierContinuousParzen 
{
	//private NormalDistribution[] churnDistributions;
	//private NormalDistribution[] nonChurnDistributions;
	private ArrayList<IDistributionDoubleNumber<Double>> churnDistributions = new ArrayList<>();
	private ArrayList<IDistributionDoubleNumber<Double>> nonChurnDistributions = new ArrayList<>();
	
	public NaiveBayesClassifierContinuousParzen(double[][] data, int[] churn, double threshold, String[] cat)
	{
		DoubleSamplingDomain domain = new DoubleSamplingDomain(-500, 500, 1000);
		System.out.println("data length"+data.length);
		System.out.println("churners length"+churn.length);
		double[][] churners = selectData(data,churn,1);
		
		for (int j=0;j<data[0].length;j++)
		{
			ParzenDistributionGenerator sdf = new ParzenDistributionGenerator(6.0);
			List<Double> l = new ArrayList<>();
			for (int i=0;i<churners.length;i++)
				if (churners[i][j]!=0) l.add(churners[i][j]);
			IDistributionDoubleNumber<Double> generate_distribution = sdf.generate_distribution(l);
			churnDistributions.add(j,generate_distribution);
		}
		//PlottingTools.plot(generate_distribution.get_density_function(), domain);

		/*double[] averages = generateAverages(churners);
		double[] deviations = generateDeviations(churners, averages);
		
		churnDistributions = new NormalDistribution[averages.length];
		for (int i=0;i<averages.length;i++)
			churnDistributions[i] = new NormalDistribution(averages[i], deviations[i]);*/
		
		double[][] nonchurners = selectData(data,churn,0);
		
		/*ParzenDistributionGenerator sdf2 = new ParzenDistributionGenerator(6.0);
		List<Double> l2 = new ArrayList<>();
		for (int i=0;i<nonchurners.length;i++)
			if (nonchurners[i][0]!=0) l2.add(nonchurners[i][0]);
		INumberDistribution<Double> generate_distribution2 = sdf2.generate_distribution(l2);
		PlottingTools.plot(generate_distribution2.get_density_function(), domain);*/
		
		//averages = generateAverages(nonchurners);
		//deviations = generateDeviations(nonchurners, averages);
		
		//nonChurnDistributions = new NormalDistribution[averages.length];
		//for (int i=0;i<averages.length;i++)
			//nonChurnDistributions[i] = new NormalDistribution(averages[i], deviations[i]);
						
		//List<IFunction<Double, Double>> functions = new ArrayList<>();
		/*List<Pair<IFunction<Double, Double>,String>> functions = new ArrayList<>();
		functions.add(new Pair<IFunction<Double,Double>, String>(generate_distribution.get_density_function(),"Churn"));
		functions.add(new Pair<IFunction<Double,Double>, String>(generate_distribution2.get_density_function(),"NonChurn"));
		PlottingTools.plotNamedFunctions(functions, domain);*/
		
		for (int j=0;j<data[0].length;j++)
		{
			ParzenDistributionGenerator sdf = new ParzenDistributionGenerator(6.0);
			List<Double> l = new ArrayList<>();
			for (int i=0;i<nonchurners.length;i++)
				if (nonchurners[i][j]!=0) l.add(nonchurners[i][j]);
			IDistributionDoubleNumber<Double> generate_distribution = sdf.generate_distribution(l);
			nonChurnDistributions.add(j,generate_distribution);
		}
		
		//for (int i=0;i<averages.length;i++)
		//{
		//	System.out.println("Category: "+cat[i]);
		//	System.out.println("Churn: "+churnDistributions[i]);
		//	System.out.println("Non-Churn: "+nonChurnDistributions[i]);
//			IFunction<Double, Double> function1 = churnDistributions[i].get_density_function();
//			IFunction<Double, Double> function2 = nonChurnDistributions[i].get_density_function();
//			List<IFunction<Double, Double>> functions = new ArrayList<>();
//			functions.add(function1);
//			functions.add(function2);
			//PlottingTools.plot(functions, domain);

		//	System.out.println();
		//}
		
		this.threshold=threshold;
	}

	private double[][] selectData(double[][] data, int[] churners, int criterium)
	{
		int count=0;
		for (int i=0;i<churners.length;i++)
			if (churners[i]==criterium) count++;
		
		double[][] result = new double[count][data[0].length];
		
		int count2=0;
		for (int i=0;i<data.length;i++)
		{
			if (churners[i]==criterium)
			{
				System.arraycopy(data[i], 0, result[count2], 0, data[i].length);
				count2++;
			}
		}
			
		return result;
	}
	
	private double[] generateAverages(double[][] data)
	{
		double[] average = new double[data[0].length];
		int[] count = new int[data[0].length];
		
		for (int i=0;i<data.length;i++)
			for (int j=0;j<data[i].length;j++)
			{
				if (data[i][j]!=0)
				{
					average[j]+=data[i][j];
					count[j]++;
				}
			}
			
		for (int j=0;j<average.length;j++)
			average[j]/=count[j];
		
		return average;
	}
	
	private double[] generateDeviations(double[][] data, double[] average)
	{
		double[] deviation = new double[data[0].length];
		int[] count = new int[data[0].length];
		
		for (int i=0;i<data.length;i++)
			for (int j=0;j<data[i].length;j++)
			{	
				if (data[i][j]!=0)
				{
					double difference = data[i][j] - average[j];
					deviation[j]+=difference*difference;
					count[j]++;
				}
			}
			
		for (int j=0;j<average.length;j++)
			deviation[j]=Math.sqrt(deviation[j]/(count[j]-1));
		
		return deviation;
	}
	
	public FMeasure classify(double[][] instances, int[] churn)
	{
		FMeasure f = new FMeasure();
		
		for (int i =0;i<churn.length;i++)
		{
			if (i%10000==0)System.out.println(i);
			double[] instance = instances[i];
			int result = classify(instance);
			
			if (churn[i]==0)
			{
				if (result==0) f.add_correct_negative();
				else f.add_false_positive();
			}
			if (churn[i]==1)
			{
				if (result==0) f.add_false_negative();
				else f.add_correct_positive();
			}
		}
		
		return f;
	}
	
	public int classify(double[] instance)
	{
		double nonChurnChance = 1;
		double churnChance = 1;
		
		for (int i=0;i<instance.length;i++)
		{
			if (instance[i]!=0)
			{
				nonChurnChance *= nonChurnDistributions.get(i).get_probability_density(instance[i]);
				churnChance *= churnDistributions.get(i).get_probability_density(instance[i]);
				
				//System.out.println(nonChurnChance + "\t"+churnChance);
			}
		}
		
		if (churnChance>(nonChurnChance*threshold)) return 1;
		return 0;
	}
	
	
	
	public double getThreshold() {
		return threshold;
	}

	public void setThreshold(double threshold) {
		this.threshold = threshold;
	}



	public double threshold = 3;
}
